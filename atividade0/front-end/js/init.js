$(function () {
    $("#includedHeader").load("header.html");
    $("#includedContent").load("aside.html", function () {
        $("li a ").removeClass("is-active");
        var menuActive = $('main').data('active');
        $("li a[href='" + menuActive + ".html'] ").addClass("is-active");
    });

    function toggleModalClasses(event) {
        var modal = $('.modal');
        $('#userNameDrop').text($(this).data('register-name'));
        $('#idNameDrop').val($(this).data('register-id'));
        modal.toggleClass('is-active');
        $('html').toggleClass('is-clipped');
    };

    // $('.open-modal').on('click', toggleModalClasses);
    $(document).on('click', '.open-modal', toggleModalClasses);
    $('.close-modal').on('click', toggleModalClasses);

    $(".send-action").on('click', function () {
        let id = $("#idNameDrop").val();
        var url = "http://localhost:5000/api/async/" + id;

        toggleModalClasses();

        $.ajax({
            url: url,
            method: "DELETE"
        }).done(function (msg) {
            $("tbody").find("[data-register-id='" + id + "']").parent().parent().remove();
        }).fail(function (e) {
            console.log(e);
        });
    });

    if ($('table').length) {
        $.get("http://localhost:5000/api/async").done(function (dados) {
            dados.forEach(function (valor) {
                var tr = document.createElement('tr');

                var td = document.createElement('td');
                var text = document.createTextNode(`${valor.id}`);
                td.append(text);
                tr.append(td);

                var td = document.createElement('td');
                var text = document.createTextNode(`${valor.descricao}`);
                td.append(text);
                tr.append(td);

                var tdacao = document.createElement('td');

                tdacao.innerHTML = `<a href="especialidades-form.html" class="button is-warning is-outlined">
                <span>Edit</span>
                <span class="icon is-small">
                    <i class="far fa-edit"></i>
                </span>
            </a>

            <a class="button is-danger is-outlined open-modal" data-register-id="${valor.id}" data-register-name="${valor.descricao}">
                <span>Delete</span>
                <span class="icon is-small">
                    <i class="far fa-trash-alt"></i>
                </span>
            </a>
            </td>`;
                tr.append(tdacao);
                document.querySelector("table > tbody").append(tr);
            });
        });
    }

    $("form").on("submit", function (event) {
        event.preventDefault();
        var datastring = $(this).serialize();
         var url = "http://localhost:5000/api/async";
         $.ajax({
             url: url,
             method: "POST",
             data: datastring,
             dataType: "json"
         }).done(function (msg) {
             window.location.href = "http://localhost:8080/especialidades.html";
         }).fail(function (e) {
             console.log(e);
         });
     });
    
});
