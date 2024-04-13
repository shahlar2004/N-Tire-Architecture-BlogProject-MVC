

$(document).ready(function () {

    var yearlyArticlesUrl =app.Urls.yearlyArticlesUrl;
    var getTotalyArticleCountUrl = app.Urls.getArticleCountUrl;
    var getTotalyCategoryCountUrl = app.Urls.getCategoryCountUrl;
    

    $.ajax({
        type: "GET",
        url: getTotalyArticleCountUrl,
        dataType: "json",
        success: function (data) {
            $("h3#articlecount").append(data);
        },
        error: function (data) {
            toastr.error("Analiz zamanı uyğunsuzluq yaranmışdır!", "Xətalı");
        }
    });

    $.ajax({
        type: "GET",
        url: getTotalyCategoryCountUrl,
        dataType: "json",
        success: function (data) {
            $("h3#categorycount").append(data);
        },
        error: function (data) {
            toastr.error("Analiz zamanı uyğunsuzluq yaranmışdır!", "Xətalı");
        }
    });



    $.ajax({
        type: "GET",
        url: yearlyArticlesUrl,
        dataType: "json",
        success: function (data) {
            var parsedData = JSON.parse(data);
            const ctx = document.getElementById('BarChart');
            new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: ['Yanvar', 'Fevral', 'Mart', 'Aprel', 'May', 'Iyun', 'Iyul', 'Avqust', 'Sentyabr', 'Oktyabr', 'Noyabr', 'Dekabr'],
                    datasets: [{
                        label: 'Say',
                        data: parsedData,
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    }
                }
            });
        },
          error: function (data) {

            alert(data  )
        }
    });
})

 