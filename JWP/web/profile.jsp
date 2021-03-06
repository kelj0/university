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
        <h1>Welcome <%= request.getSession().getAttribute("user") %>, here are your purchases: </h1>
        <div class="shop__products">
            <div class="purchases">
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
        $.get("/api/get_purchases/" + '<%= request.getSession().getAttribute("uuid")%>').done(function(d){fill_purchases(d)})
    };

    function fill_purchases(data) {
        data.forEach(purchase => {
            $(".purchases").append("<div>Purchase[" + purchase.split('|')[0] + "] total: " + purchase.split('|')[1] + "<br></div>");
        })
    }
</script>

</body>
