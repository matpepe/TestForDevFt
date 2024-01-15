// translation.js

function googleTranslateElementInit() {
    new google.translate.TranslateElement(
        {
            pageLanguage: 'en', // Default language
            includedLanguages: 'en,hr,sr,sl,de,ru,zh-CN', // English, Croatian, Serbian, Slovenian, German, Russian, Chinese
            layout: google.translate.TranslateElement.InlineLayout.SIMPLE, // or other layout as needed
            autoDisplay: true
        },
        'google_translate_element'
    );
}


//function googleTranslateElementInit() {
//    new google.translate.TranslateElement(
//        { pageLanguage: 'en' },
//        'google_translate_element'
//    );
//}

