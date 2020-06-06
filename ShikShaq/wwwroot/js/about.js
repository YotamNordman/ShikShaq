var map;
initCanvas();
// Adds a marker to the map.
function addMarker(location, map, title) {
    // Add the marker at the clicked location, and add the next-available label
    // from the array of alphabetical characters.
    var marker = new google.maps.Marker({
        position: location,
        animation: google.maps.Animation.DROP,
        title: title,
        map: map
    });

    marker.setMap(map);
    marker.addListener('click', toggleBounce);
}

function toggleBounce() {
    if (this.getAnimation() !== null) {
        this.setAnimation(null);
    } else {
        this.setAnimation(google.maps.Animation.BOUNCE);
    }
}

function initMap() {
    // Center map at college of management
    mapCenter = { lat: 31.969738, lng: 34.7749759 };
    mapCenter2 = { lat: 31.979838, lng: 34.7648759 };

    map = new google.maps.Map(document.getElementById('map'), {
        center: mapCenter,
        zoom: 15
    });

    getBranchesLocations();
};

function getBranchesLocations() {
    $.ajax({
        type: "GET",
        url: '../Branches/Location',
        data: {},
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
        },
        success: function (response) {
            var branchLocation = {};
            for (branchIndex in response) {
                branchLocation = {
                    lat: response[branchIndex].lat,
                    lng: response[branchIndex].lng
                };

                addMarker(branchLocation, map, response[branchIndex].name);
            }
        },
        complete: function () {
        },
        failure: function (jqXHR, textStatus, errorThrown) {
            alert("HTTP Status: " + jqXHR.status + "; Error Text: " + jqXHR.responseText); // Display error message
        }
    });
}

function initCanvas() {
    var c = document.getElementById("titleCanvas");
    var ctx = c.getContext("2d");

    ctx.font = "55px Verdana";
    // Create gradient
    var gradient = ctx.createLinearGradient(0, 0, c.width, 0);
    gradient.addColorStop("0", "#090979");
    gradient.addColorStop("1.0", "#00d4ff");
    // Fill with gradient
    ctx.fillStyle = gradient;
    ctx.fillText("Shik-Shaq", 10, 90);
}