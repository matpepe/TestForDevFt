$(document).ready(function () {
    // Default language
    let selectedLanguage = 'en';

    // Function to set translations based on the selected language
    function setTranslations() {
        $('[data-en]').each(function () {
            const translationKey = $(this).attr(`data-${selectedLanguage}`);
            $(this).text(translationKey);
        });
    }

    // Initial translation
    setTranslations();

    // Event listener for language change
    $('#languageSelect').change(function () {
        selectedLanguage = $(this).val();
        setTranslations();
    });
});
