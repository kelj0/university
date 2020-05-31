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
                <jsp:param name="title" value="Checkout"/>
            </jsp:include>
        </div>
        <!-- END HEADER TAG -->

        <!-- BODY TAG -->
        <div id="base">

        </div>
        <!-- END BODY TAG -->
    </section>
    <!-- FOOTER TAG -->
    <div id="pagefooter">
        <%@include file="shared/footer.jsp"%>
    </div>
    <!-- END FOOTER TAG -->

</body>
