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
    var defaultBranchLocation = { lat: 31.970066, lng: 34.773055 };
    $("#branchLat").val(defaultBranchLocation.lat);
    $("#branchLng").val(defaultBranchLocation.lng);

    var map = new google.maps.Map(
        document.getElementById('map'),
        { zoom: 15, center: defaultBranchLocation }
    );

    var marker = createMarker(defaultBranchLocation, map, $("#branchName").val());

    map.addListener('click', function (mapsMouseEvent) {
        marker.setPosition(mapsMouseEvent.latLng);
        $("#branchLat").val(mapsMouseEvent.latLng.lat());
        $("#branchLng").val(mapsMouseEvent.latLng.lng());
    });

}