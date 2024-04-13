
$(document).ready(function () {

    $(".btnSave").click(function (event) {

        event.preventDefault();

        var addUrl = app.Urls.categoryAddUrl;
        var redirectUrl = app.Urls.articleAddUrl;

        var name = $(".categoryName").val();

        var categoryAddDTO = {
            Name: name
        }

        var jsonData = JSON.stringify(categoryAddDTO);

        $.ajax({
            url: addUrl,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: jsonData,
            success: function (data) {
                 setTimeout(function () {
               // alert(data);
                    window.location.href = redirectUrl;
              }, 1500);
            },
            error: function (data) {

                alert(data.error)
            }
        });
    });
});