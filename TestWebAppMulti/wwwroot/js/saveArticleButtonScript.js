// script.js

$(document).ready(function () {
    $('#saveArticleButton').on('click', function (e) {
        e.preventDefault(); // Prevent the default behavior (following the link)

        // Construct the URL directly or use data-* attribute
        var url = '@Url.Action("SaveArticle", "CryptoArticle")';

        // Make an AJAX request to the SaveArticle action
        $.ajax({
            url: url,
            type: 'POST',
            success: function (data) {
                // Handle success if needed
                console.log('Article saved successfully.');
            },
            error: function (error) {
                // Handle error if needed
                console.error('Error saving article:', error);
            }
        });
    });
}); // preventing redirection
