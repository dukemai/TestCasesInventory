tinymce.init({
    selector: 'textarea',
    height: 150,
    width: '85%',
    theme: 'modern',
    plugins: [
      'advlist autolink link image preview hr',
      'paste textcolor colorpicker imagetools'
    ],
    toolbar1: 'insertfile undo redo | styleselect | bold italic underline| alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image | preview media | forecolor backcolor',
    image_advtab: true,
    content_css: [
      '//fonts.googleapis.com/css?family=Lato:300,300i,400,400i',
      '//www.tinymce.com/css/codepen.min.css'
    ]
});
