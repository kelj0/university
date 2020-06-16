let cart_open = false;
let items_in_cart = [];
let items = [];

$(document).ready(get_products());
$(document).ready(populate_dropdown());
$("#categories").on('change', filter_products)

String.format = function() {
    let s = arguments[0];
    for (let i = 0; i < arguments.length - 1; i += 1) {
        let reg = new RegExp('\\{' + i + '\\}', 'gm');
        s = s.replace(reg, arguments[i + 1]);
    }
    return s;
};

function filter_products() {
    let cat_uuid = this.value;
    items.forEach(
       function(p) {
           if(cat_uuid==="All" || cat_uuid === p.category_id){
               $("#"+p.uuid).removeClass('hide');
           }else{
               $("#"+p.uuid).addClass('hide');
           }
       }
    );
    if(cat_uuid !=="All") {
        show_msg("Showing only " + $("#categories option:selected").text())
    }
}

function populate_dropdown(){
    $.getJSON("/api/get_categories", function(result) {
        let options = $("#categories");
        [...result].forEach(
            c => options.append($("<option />").val(c.uuid).text(c.category))
        )
    });
}

function get_products(){
    $.get("/api/get_products").done(function(d){fill_products(d)})
}

function fill_products(data) {
    data.forEach(product => {
        let html =
        "<div id=\"" + product.uuid  + "\" class=\"products__item\">\n  <article class=\"product\">\n    <h1 class=\"product__title\">" + product.name + "</h1>\n    <p class=\"product__text\">\n$" + product.price +"   </p>\n  <a class=\"button js-add-product " + product.uuid + "\" href=\"#\" title=\"Add to cart\" onclick=\"add_to_cart('" + product.uuid + "', '" + product.name + "', " + product.price + ", '" + product.category_id + "')\">\n Add to cart\n </a>\n </article>\n </div>"
        $(".products").append(html);
        items.push(product);
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

function add_to_cart(uuid, name, price, category_id) {
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
                'price': price,
                'category_id': category_id
            });
        localStorage.setItem("items_in_cart", JSON.stringify(items_in_cart));
    }else{
        $("#"+uuid).css("border","");
        $("."+uuid).text("Add to cart");
        $("."+uuid).css("background-color", "#39c");
        $('.js-cart-empty').addClass('hide');
        let ind = 0;
        for(let i=0; i < items_in_cart.length; ++i){
            if(uuid === items_in_cart[0]['uuid']){ind=i;break}
        }
        items_in_cart.splice(ind,1);
        localStorage.setItem("items_in_cart", JSON.stringify(items_in_cart));
        console.log("REMOVED ITEM ID: " + uuid);
    }
    if(localStorage.getItem("items_in_cart").length > 2){
        $("#cart_button").removeClass("hide")
    }else{
        $("#cart_button").addClass("hide")
    }
}
