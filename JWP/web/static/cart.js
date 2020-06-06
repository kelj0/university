var cart_open = false;
var numberOfProducts = 0;
var items = null;
var items_in_cart = [];


$(document).ready(get_products());

String.format = function() {
    var s = arguments[0];
    for (var i = 0; i < arguments.length - 1; i += 1) {
        var reg = new RegExp('\\{' + i + '\\}', 'gm');
        s = s.replace(reg, arguments[i + 1]);
    }
    return s;
};

function get_products(){
    $.get("http://localhost:8080/api/get_products").done(function(d){fill_products(d)})
}

function fill_products(data) {
    data.forEach(product => {
        let html =
        "<div id=\"" + product.uuid  + "\" class=\"products__item\">\n  <article class=\"product\">\n    <h1 class=\"product__title\">" + product.name + "</h1>\n    <p class=\"product__text\">\n" + product.price +"   </p>\n  <a class=\"button js-add-product " + product.uuid + "\" href=\"#\" title=\"Add to cart\" onclick=\"add_to_cart('" + product.uuid + "', '" + product.name + "', " + product.price + ")\">\n Add to cart\n </a>\n </article>\n </div>"
        $(".products").append(html);
    })
}

function open_cart() {
    cart_open = true;
    // 0 id, 1 name, 2 price
    JSON.parse(localStorage.getItem("items_in_cart")).forEach(
        x =>
            $('.js-cart-products').prepend("<div class=\"cart__product\">\n <article class=\"js-cart-product\">\n <h1 class=\"product_cart_template_heading\">" + x.name + " ($" + x.price + ")</h1>\n </article>\n </div>")
    )
    $('body').addClass('open');
}

function close_cart() {
    cart_open = false;
    $('body').removeClass('open');
    $('.js-cart-products').empty();
}

function add_to_cart(uuid, name, price) {
    let txt = $("."+uuid).text();
    if(txt.toString().includes("Add to cart")) {
        $("#"+uuid).css("border","3px solid green")
        $("."+uuid).text("Remove from cart");
        $("."+uuid).css("background-color", "red");
        $('.js-cart-empty').addClass('hide');
        items_in_cart.push(
            {
                'id': uuid,
                'name': name,
                'price': price
            });
        localStorage.setItem("items_in_cart", JSON.stringify(items_in_cart));
        numberOfProducts++;
        console.log("ADD ITEM ID: " + uuid);
    }else{
        $("#"+uuid).css("border","");
        $("."+uuid).text("Add to cart");
        $("."+uuid).css("background-color", "#39c");
        $('.js-cart-empty').addClass('hide');
        let ind = 0;
        for(let i=0; i < items_in_cart.length; ++i){
            if(uuid == items_in_cart[0]['uuid']){ind=i;break}
        }
        items_in_cart.splice(ind,1);
        localStorage.setItem("items_in_cart", JSON.stringify(items_in_cart));
        numberOfProducts--;
        console.log("REMOVED ITEM ID: " + uuid);
    }
}
