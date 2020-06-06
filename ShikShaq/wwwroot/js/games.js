var $;

function Show() {
    $('#div_Loader').show();
}

function Hide() {
    $('#div_Loader').hide();
}

function getScores() {
    $.ajax({
        type: "GET",
        url: 'Games/Scores',
        data: {},
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            Show(); // Show loader icon
        },
        success: function (response) {
            columns = [
                { title: "Date" },
                { title: "Home Team" },
                { title: "Away Team" },
                { title: "Score" },
                { title: "Status" }
            ];

            scoresDatasource = [];

            for (index in response.api.fixtures) {
                var fixture = response.api.fixtures[index];
                scoresDatasource.push([
                    new Date(fixture.event_date).toLocaleString(),
                    fixture.homeTeam.team_name,
                    fixture.awayTeam.team_name,
                    fixture.score.fulltime,
                    fixture.status
                ]);
            }

            $('#scoresTable').DataTable({
                data: scoresDatasource,
                columns: columns
            });
        },
        complete: function () {
            Hide(); // Hide loader ic
        },
        failure: function (jqXHR, textStatus, errorThrown) {
            alert("HTTP Status: " + jqXHR.status + "; Error Text: " + jqXHR.responseText); // Display error message
        }
    });
}

$(document).ready(function () {
    $("#div_Loader").show();
    setTimeout(() => {
        getScores();
    }, 2000);
});