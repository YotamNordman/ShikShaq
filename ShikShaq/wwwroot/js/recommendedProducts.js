var $;

function ShowProductLoader() {
    $('#product_loader').show();
}

function HideProductLoader() {
    $('#product_loader').hide();
}

function getRecommendedProducts() {
    console.log('started getRecommendedProducts');
    $.ajax({
        url: '/Products/RecommendedProducts',
        type: 'get',
        dataType: "JSON",
        processData: false,
        contentType: false,
        success: function (data) {
            createProductCards(data);
        },
        complete: function () {
            HideProductLoader();
        },
        error: function (data) {
            console.log(data);
        }
    });
}

function createProductCards(products) {
    products.forEach(function (product) {
        let card = $($('#product-template').html());

        if (product.image != null) {
            card.find('.productLogo').attr('src', 'data:image/png;base64,' + product.image);
        } else {
            card.find('.productLogo').attr('src', '/images/no-image.png');
        }        

        card.find('.product-title').text(product.name);
        card.find('.product-price').text(product.price);
        card.find('.image-link').attr('href', '/Products/Details/' + product.id);
        card.find('.details-link').attr('href', '/Products/Details/' + product.id);
        
        $('.card-list').append(card);
    });
}
$(document).ready(function () {
    setTimeout(() => {
        ShowProductLoader();

        getRecommendedProducts();
    }, 2000);
});