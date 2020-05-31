var cart_open = false;
var numberOfProducts = 0;
var items = null;
var items_in_cart = [];

String.format = function() {
    var s = arguments[0];
    for (var i = 0; i < arguments.length - 1; i += 1) {
        var reg = new RegExp('\\{' + i + '\\}', 'gm');
        s = s.replace(reg, arguments[i + 1]);
    }
    return s;
};

function fill_items() {
    // get from api
    items = [
        {
            'id': '1',
            'name': 'test1',
            'price': 10.2
        },
        {
            'id': '2',
            'name': 'test2',
            'price': 20.2
        },
        {
            'id': '3',
            'name': 'test3',
            'price': 30.2
        }
    ];
}

function open_cart() {
    cart_open = true;
    fill_items();
    var html =
        '<div class="cart__product">'+
        '   <article class="js-cart-product">'+
        '       <h1>{1} ({2})</h1>'+
        '       <p>'+
        '           <a class="js-remove-product" onclick="remove_product({0})" href="#" title="Delete product">'+
        '               Remove {1}'+
        '           </a>'+
        '       </p>'+
        '   </article>'+
        '</div>'
    // 0 id, 1 name, 2 price
    JSON.parse(localStorage.getItem("items_in_cart")).forEach(
        x =>
            $('.js-cart-products').prepend($(String.format(html,x.id, x.name, x.price)))
    )

    console.log("open cart: " + items_in_cart);
    console.log("open cart: " + localStorage.getItem("items_in_cart"));
    $('body').addClass('open');
}

function close_cart() {
    cart_open = false;
    $('body').removeClass('open');
}

function add_to_cart(id, name, price) {
    $("#"+id).css("border","3px solid green")
    $('.js-cart-empty').addClass('hide');
    items_in_cart.push(
        {
            'id': id,
            'name': name,
            'price': price
    });
    localStorage.setItem("items_in_cart", JSON.stringify(items_in_cart));
    numberOfProducts++;
    console.log("ADD_TO: " + items_in_cart);
}

function remove_product(id) {
    $(id).css("border","none")
    numberOfProducts--;
    $(this).closest('.js-cart-product').hide(250);
    if(numberOfProducts == 0) {
        $('.js-cart-empty').removeClass('hide');
    }
}