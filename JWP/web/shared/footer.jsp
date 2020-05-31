<aside class="cart js-cart">
    <div class="cart__header">
        <h1 class="cart__title">Shopping cart</h1>
        <p class="cart__text">
            <a class="button button--light js-toggle-cart" href="#" title="Close cart">
                Close cart
            </a>
        </p>
    </div>
    <div class="cart__products js-cart-products">
        <p class="cart__empty js-cart-empty">
            Add a product to your cart
        </p>
        <div class="cart__product js-cart-product-template">
            <article class="js-cart-product">
                <h1>Product title</h1>
                <p>
                    <a class="js-remove-product" href="#" title="Delete product">
                        Delete product
                    </a>
                </p>
            </article>
        </div>
    </div>
    <div class="cart__footer">
        <p class="cart__text">
            <a class="button" href="/checkout" title="Buy products">
                Buy products
            </a>
        </p>
    </div>
</aside>

<div class="lightbox js-lightbox js-toggle-cart"></div>



<!--   Bootstrap  -->
<script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>
<!-- end bootstrap -->
<script src="${pageContext.request.contextPath}/static/card.js"></script>

