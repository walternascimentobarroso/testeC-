$(function () {
    $("#includedContent").load("aside.html");

    function toggleModalClasses(event) {
        var modal = $('.modal');
        $('#userNameDrop').text($(this).data('register-name'));
        $('#idNameDrop').val($(this).data('register-id'));
        modal.toggleClass('is-active');
        $('html').toggleClass('is-clipped');
    };

    $('.open-modal').click(toggleModalClasses);

    $('.close-modal').click(toggleModalClasses);

    $(".send-action").on('click', function () {
        let id = $("#idNameDrop").val();
        var url = "remover-cliente/" + id;

        toggleModalClasses();

        $.post(url)
            .done(function (msg) {
                $("tbody").find("[data-register-id='" + id + "']").parent().parent().remove();
            })
            .fail(function (e) {
                console.log(e);
            }
            );
    });
});
