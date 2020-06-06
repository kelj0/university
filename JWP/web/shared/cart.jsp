<aside class="cart js-cart">
    <div class="cart__header">
        <h1 class="cart__title">Shopping cart</h1>
        <p class="cart__text">
            <a class="button button--light js-toggle-cart" href="#" onclick="close_cart()" title="Close cart">
                Close cart
            </a>
        </p>
    </div>
    <div class="cart__products js-cart-products">
        <p class="cart__empty js-cart-empty">
            Add a product to your cart
        </p>

    </div>
    <div class="cart__footer">
        <p class="cart__text">
            <a class="button" href="/checkout" title="Buy products">
                Buy products
            </a>
        </p>
    </div>
</aside>

<div onload="fill_items()" class="lightbox js-lightbox js-toggle-cart"></div>
