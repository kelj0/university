cart_items = []

jQuery(document).ready(get_cart_items())
    .then(generate_user_cart())

function get_cart_items(){
    this.cart_items = JSON.parse(localStorage.getItem("items_in_cart"))
}

function handle_checkout(){
    const form = document.createElement('form');
    form.method = "post";
    form.action = "/checkout";
    var s = "";
    [...this.cart_items].forEach(p=>s+=(p.id+"|"))
    const hiddenField = document.createElement('input');
    hiddenField.type = 'hidden';
    hiddenField.name = "items";
    hiddenField.value = s;
    form.appendChild(hiddenField);
    document.body.appendChild(form);
    form.submit();
}

function generate_user_cart() {
    let sum = 0;
    [...cart_items].forEach(product => {
        let html = "<div class='cart_item'>\n  <h1>" + product.name + "</h1>\n  <h2>Price: " + product.price + "</h2>\n</div>\n"
        jQuery("#cart_items").append(html);
        sum+=product.price
    })
    $("#cart_items").append("<h2>TOTAL: " + sum + "</h2>")
}