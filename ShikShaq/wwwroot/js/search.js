$(function () {
    var form = $('#searchForm');
    var url = form.attr('action');

    $('#name,#height,#address').keyup(function () {
        $.ajax(
            {
                url: url,
                data: form.serialize(),
                success: function (res) {
                    $('tbody').html(res);
                }

            });
    });
});