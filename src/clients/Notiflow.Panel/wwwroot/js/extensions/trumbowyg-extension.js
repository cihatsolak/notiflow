const inputConvertToTrumbowyg = (id) => {
    $(`#${id}`).trumbowyg({
        lang: 'tr',
        semantic: false,
        btnsDef: {
            image: {
                dropdown: ['insertImage', 'upload'],
                ico: 'insertImage'
            }
        },
        btns: [
            ['undo', 'redo'],
            ['formatting'],
            ['strong', 'em', 'del'],
            ['superscript', 'subscript'],
            ['link'],
            ['image'],
            ['justifyLeft', 'justifyCenter', 'justifyRight', 'justifyFull'],
            ['unorderedList', 'orderedList'],
            ['horizontalRule'],
            ['removeformat'],
            ['fullscreen'],
            ['fontsize'],
            ['foreColor', 'backColor'],
            ['fontfamily'],
            ['indent', 'outdent'],
            ['lineheight'],
            ['table']
        ],
        tagsToRemove: ['script', 'link'],
        autogrow: true,
        plugins: {
            fontsize: {
                sizeList: [
                    '10px',
                    '13px',
                    '16px',
                    '19px',
                    '22px',
                    '25px',
                    '28px'
                ],
                allowCustomSize: false
            },
            resizimg: {
                minSize: 64,
                step: 16,
            },
            fontfamily: {
                fontList: [
                    { name: 'Arial', family: 'Arial, Helvetica, sans-serif' },
                    { name: 'Arial Black', family: 'Arial Black, Gadget, sans-serif' },
                    { name: 'Comic Sans', family: 'Comic Sans MS, Textile, cursive, sans-serif' },
                    { name: 'Courier New', family: 'Courier New, Courier, monospace' },
                    { name: 'Georgia', family: 'Georgia, serif' },
                    { name: 'Impact', family: 'Impact, Charcoal, sans-serif' },
                    { name: 'Lucida Console', family: 'Lucida Console, Monaco, monospace' },
                    { name: 'Lucida Sans', family: 'Lucida Sans Uncide, Lucida Grande, sans-serif' },
                    { name: 'Palatino', family: 'Palatino Linotype, Book Antiqua, Palatino, serif' },
                    { name: 'Tahoma', family: 'Tahoma, Geneva, sans-serif' },
                    { name: 'Times New Roman', family: 'Times New Roman, Times, serif' },
                    { name: 'Trebuchet', family: 'Trebuchet MS, Helvetica, sans-serif' },
                    { name: 'Verdana', family: 'Verdana, Geneva, sans-serif' }
                ]
            },
            upload: {
                serverPath: '/common/fileupload',
                fileFieldName: 'formFile',
                headers: {
                    'x-folder-name': `${$(location).attr('href').split('/')[3]}s`.toLowerCase(),
                    'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
                },
                urlPropertyName: 'path'
            }
        }
    });
}
