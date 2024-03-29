﻿/* Set rates + misc */
var taxRate = 0.05;
var shippingRate = 15.00;
var fadeTime = 300;
recalculateCart();


/* Assign actions */
$('.product-quantity input').change(function () {
    updateQuantity(this);
});

$('.product-removal button').click(function () {
    removeItem(this);
});

$('.clear').click(function () {
    removeAllItems();
});

$('.checkout').click(function () {
    checkoutOrder();
});

$('.save').click(function () {
    saveChanges();
});


/* Recalculate cart */
function recalculateCart() {
    var subtotal = 0;

    /* Sum up row totals */
    $('.product').each(function () {
        subtotal += parseFloat($(this).children('.product-line-price').text());
    });

    /* Calculate totals */
    var tax = subtotal * taxRate;
    var shipping = (subtotal > 0 ? shippingRate : 0);
    var total = subtotal + tax + shipping;

    /* Update totals display */
    $('.totals-value').fadeOut(fadeTime, function () {
        $('#cart-subtotal').html(subtotal.toFixed(2));
        $('#cart-tax').html(tax.toFixed(2));
        $('#cart-shipping').html(shipping.toFixed(2));
        $('#cart-total').html(total.toFixed(2));
        if (total == 0) {
            $('.checkout').fadeOut(fadeTime);
        } else {
            $('.checkout').fadeIn(fadeTime);
        }
        $('.totals-value').fadeIn(fadeTime);
    });
}


/* Update quantity */
function updateQuantity(quantityInput) {
    /* Calculate line price */
    var productRow = $(quantityInput).parent().parent();
    var price = parseFloat(productRow.children('.product-price').text());
    var quantity = $(quantityInput).val();
    var linePrice = price * quantity;

    /* Update line price display and recalc cart totals */
    productRow.children('.product-line-price').each(function () {
        $(this).fadeOut(fadeTime, function () {
            $(this).text(linePrice.toFixed(2));
            recalculateCart();
            $(this).fadeIn(fadeTime);
        });
    });
}


/* Remove item from cart */
function removeItem(removeButton) {
    /* Remove row from DOM and recalc cart total */
    var productRow = $(removeButton).parent().parent();
    productRow.slideUp(fadeTime, function () {
        productRow.remove();
        recalculateCart();
    });
}

/* Remove all items from cart */
function removeAllItems() {
    $('.remove-product').each(function () {
        removeItem($(this));
    });
}

/* Delete cart and create an order from it */
function checkoutOrder() {
    var finishOrder = {};
    finishOrder.FinalPrice = parseInt($('#cart-subtotal').text());
    finishOrder.ProductInOrders = [];

    finishOrder.BranchId = $('#slct').val();

    if (finishOrder.BranchId != null && !isNaN(finishOrder.BranchId)) {
        finishOrder.BranchId = Number.parseInt(finishOrder.BranchId);

        $('.product').each(function () {
            var currProduct = new Object();
            currProduct.ProductId = this.id;
            currProduct.Quantity = $('#' + this.id + ' .product-quantity input').val();
            finishOrder.ProductInOrders.push(currProduct);
        });

        if (finishOrder.ProductInOrders.length > 0) {
            $.ajax('/Account/CheckoutOrder', {
                type: 'POST',
                data: JSON.stringify(finishOrder),
                contentType: 'application/json',
                success: function (data, status, xhr) {
                    removeAllItems();
                    $('#status-message').html('Order has been created successfully!');
                },
                error: function (jqXhr) {
                    if (jqXhr.status == 500) {
                        $('#status-message').html('Error while creating the order! call an administrator to fix it!');
                    } else {
                        $('#status-message').html(jqXhr.responseJSON.error);
                    }
                }
            });
        } else {
            $('#status-message').html('You cannot order without products!');
        }
    } else {
        $('#status-message').html('You must choose a branch!');
    }

   

   
}

/* Saves the cart current state*/
function saveChanges() {

    var savedProductArray = [];
    $('.product').each(function () {
        var currProduct = new Object();
        currProduct.ProductId = this.id;
        currProduct.Quantity = $('#' + this.id + ' .product-quantity input').val();
        savedProductArray.push(currProduct);
    });

    $.ajax('/Account/SaveCart', {
        type: 'POST', 
        data: JSON.stringify(savedProductArray),
        contentType: 'application/json',
        success: function (data, status, xhr) {
            $('#status-message').html('Cart saved successfully!');
        },
        error: function (jqXhr, textStatus, errorMessage) {
            $('#status-message').html('Error while saving the cart current state! call an administrator to fix it!');
        }
    });

}