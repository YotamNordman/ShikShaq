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
    var branchLocation = { lat: 0, lng: 0 };

    if ($("#branchLat").text() != "" && $("#branchLng").text() != "") {
        branchLocation = {
            lat: parseFloat($("#branchLat").text()),
            lng: parseFloat($("#branchLng").text())
        };
    }

    var map = new google.maps.Map(
        document.getElementById('map'),
        { zoom: 15, center: branchLocation }
    );

    createMarker(branchLocation, map, $("#branchName").val());
}