<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html>
<head>
    <%@include file="shared/static.jsp"%>
</head>
<body>
    <section class="shop">
        <!-- HEADER TAG -->
        <div id="pageheader">
            <jsp:include page="shared/header.jsp">
                <jsp:param name="title" value="Home"/>
            </jsp:include>
        </div>
        <!-- END HEADER TAG -->

        <!-- BODY TAG -->
        <div id="base">
            <div class="shop__products">
                <div class="products">
                    <% for(int i = 0; i < 10; ++i) { %>
                        <div id="<%=i%>" class="products__item">
                            <article class="product">
                                <h1 class="product__title">Product <%=i%></h1>
                                <p class="product__text">
                                    <a class="button js-add-product" href="#" onclick="add_to_cart(<%=i%>, 'product 1', 10.2)" title="Add to cart">
                                        Add to cart
                                    </a>
                                </p>
                            </article>
                        </div>
                    <%}%>
                </div>
            </div>
        </div>
        <!-- END BODY TAG -->
    </section>
    <!-- FOOTER TAG -->
    <div id="pagefooter">
        <%@include file="shared/footer.jsp"%>
    </div>
    <!-- END FOOTER TAG -->

</body>
