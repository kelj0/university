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
                <!-- FILL WITH JS -->
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
<script>
    $(document).ready(get_purchases());
    function get_purchases(){
        $.get("/api/get_purchases/" + <%= request.getSession().getAttribute("uuid")%>).done(function(d){fill_purchases(d)})
    };

    function fill_purchases(data) {
        data.forEach(purchase => {
        let html = "<div>Purchase total: " + purchase.total + "</div>"
        $(".products").append(html);
            items.push(product);
        })
    }
</script>

</body>
