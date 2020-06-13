function createMarker(location, map, title) {
    var marker = new google.maps.Marker({
        position: location,
        animation: google.maps.Animation.DROP,
        title: title,
        map: map
    });

    marker.setMap(map);

    return marker;
}

function initMap() {
    branchLocation = {};

    if ($("#branchLat").val() == "" && $("#branchLng").val() == "") {
        branchLocation = { lat: 31.970066, lng: 34.773055 };
        $("#branchLat").val(branchLocation.lat);
        $("#branchLng").val(branchLocation.lng);
    } else {
        branchLocation = {
            lat: parseFloat($("#branchLat").val()),
            lng: parseFloat($("#branchLng").val())
        };
    }

    var map = new google.maps.Map(
        document.getElementById('map'),
        { zoom: 15, center: branchLocation }
    );    

    var marker = createMarker(branchLocation, map, $("#branchName").val());

    map.addListener('click', function (mapsMouseEvent) {
        marker.setPosition(mapsMouseEvent.latLng);
        $("#branchLat").val(mapsMouseEvent.latLng.lat());
        $("#branchLng").val(mapsMouseEvent.latLng.lng());
    });

}