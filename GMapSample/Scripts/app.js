$(document).ready(function () {
    $("input[type='checkbox']").change(function () {
        findAddress();
    });

    $('#addr').bind("keypress", function (e) {
        if (e.keyCode == 13) {
            findAddress();
            return false; // prevent the button click from happening
        }
    });

    $('#save').bind("click", function (e) {
        e.preventDefault();

        saveResult();
    });
});


var geocoder;
var map;
var markers = Array();
var infos = Array();

function initialize() {
    // prepare Geocoder
    geocoder = new google.maps.Geocoder();

    // set initial position (Denver, Colorado)
    var myLatlng = new google.maps.LatLng(39.7392358, -104.990251);

    var myOptions = { // default map options
        zoom: 12,
        center: myLatlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    map = new google.maps.Map(document.getElementById('map'), myOptions);

    var marker = new google.maps.Marker({
        map: map,
        position: myLatlng,
    });
}

// clear overlays function
function clearOverlays() {
    if (markers) {
        for (i in markers) {
            markers[i].setMap(null);
        }
        markers = [];
        infos = [];
    }
}

// clear infos function
function clearInfos() {
    if (infos) {
        for (i in infos) {
            if (infos[i].getMap()) {
                infos[i].close();
            }
        }
    }
}

// find address function
function findAddress() {

    clearPlaces();

    var address = document.getElementById("addr").value;

    // script uses our 'geocoder' in order to find location by address name
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) { // and, if everything is ok

            // we will center map
            var addrLocation = results[0].geometry.location;
            map.setCenter(addrLocation);

            // and then - add new custom marker
            var addrMarker = new google.maps.Marker({
                position: addrLocation,
                map: map,
                title: results[0].formatted_address,
                //icon: 'marker.png'
            });

            findPlaces(results[0].geometry.location);

        } else {
            alert('Geocode was not successful for the following reason: ' + status);
        }
    });
}

// find custom places function
function findPlaces(cur_location) {
    //get places based on checked place types
    var selectedTypes = getSelectedPlaceTypes();

    if (selectedTypes.length > 0) {
        $('#right-panel').show();

        //convert miles to meters
        var radius = parseInt($('#radius').val()) * 1609.34;

        // send request
        var service = new google.maps.places.PlacesService(map);
      
        service.nearbySearch({
            location: cur_location,
            radius: radius,
            types: selectedTypes,
        }, processResults);
    }
    else {
        $('#right-panel').hide();
    }
}

function processResults(results, status, pagination) {
    if (status !== google.maps.places.PlacesServiceStatus.OK) {
        return;
    } else {

        // if we have found something - clear map (overlays)
        clearOverlays();

        //and create new markers by search result
        createMarkers(results);

        if (pagination.hasNextPage) {
            var moreButton = document.getElementById('more');

            moreButton.disabled = false;

            moreButton.addEventListener('click', function () {
                moreButton.disabled = true;
                pagination.nextPage();
            });
        }
    }
}

function createMarkers(places) {
    
    var bounds = new google.maps.LatLngBounds(); 
    var placesList = document.getElementById('places');

    for (var i = 0, place; place = places[i]; i++) {
        var image = {
            url: place.icon,
            size: new google.maps.Size(71, 71),
            origin: new google.maps.Point(0, 0),
            anchor: new google.maps.Point(17, 34),
            scaledSize: new google.maps.Size(25, 25)
        };

        var mark = new google.maps.Marker({
            map: map,
            icon: image,
            title: place.name + ' ' + place.vicinity,
            position: place.geometry.location
        });
        markers.push(mark);

        //write to the right panel
        placesList.innerHTML += '<li data-place-type="' + place.place_id + '">' + place.name +
            ' at ' + place.vicinity + '</li>';

        bounds.extend(place.geometry.location);
    }
    map.fitBounds(bounds);
}

function getSelectedPlaceTypes() {
    var placeTypes = [];

    if ($('#Grocery').is(':checked'))
        placeTypes.push($('#Grocery').val());

    if ($('#School').is(':checked'))
        placeTypes.push($('#School').val());

    if ($('#DeptStore').is(':checked'))
        placeTypes.push($('#DeptStore').val());

    return placeTypes;
}

function clearPlaces() {
    $("ul").empty();
}

function saveResult() {
    showLoading();

    //add the places to an array
    var places = [];
    var listItems = $("#places li");
    listItems.each(function (idx, li) {
        var place = $(li);
        places.push({ GoogleMapPlaceID: place.attr('data-place-type'), PlaceName: place.text() });
    });

    var address = { AddressName: $('#addr').val(), RadiusMiles: $('#radius').val(), Places: places };
    address = JSON.stringify({ 'address': address });

    //post data to the server
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '/Address/Save',
        data: address,
        success: function (result) {

            hideLoading();

            if (result)
                alert("Save successful!");
            else
                alert("Failed to save.");
        },
        failure: function (response) {
            alert(response);
        }
    });
};

function showLoading() {
    $('#loading').show();
    $('#save').hide();
    $('#more').hide();
}

function hideLoading() {
    $('#loading').hide();
    $('#save').show();
    $('#more').show();
}

// initialization
google.maps.event.addDomListener(window, 'load', initialize);