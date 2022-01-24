$(document).ready(function () {

    $('.datepicker').datepicker({
        language: 'fr',
        format: 'dd/mm/yyyy',
        endDate: '0d'
    });

    $("#alertInput").on("change", "input", function () {
        $("#alertHidden").val("true");
    });
});

// Important car sinon empeche le submit à cause du format de date français
$("form").validate().settings.ignore = ".datepicker, :hidden";

function ShowAlert(type, hasTimeout) {
    $(".alert-" + type).fadeIn();

    if (hasTimeout) { setTimeout(HideAlert, 5000); }
}

function HideAlert(type) {
    if (typeof type == 'undefined') { $(".alert").fadeOut(); }
    else { $(".alert-" + type).fadeOut(); }
    $("#alertHidden").val("false");
}

function getCollaborateur(branche) {
    $.ajax({
        url: "/Home/GetCollaborateurByBranche",
        type: 'GET',
        data: { branche: branche },
        success: function (collab) {
            var html = '<option value="">Selectionner un collaborateur</option>';
            $.each(collab, function (index, elem) {
                html += '<option value="' + elem.IdCollaborateur + '">' + elem.Prenom + ' ' + elem.Nom + '</option>';
            });
            $("#Collaborateur").html(html);
        }
    })
}

function getService(id) {
    if ($("#alertHidden").val() == "true") {
        alert("Veuillez sauvegarder vos données avant de changer d'équipe");
        $("#Equipe").val($("#EquipeId").val());
        return false;
    }
    $.ajax({
        url: '@Url.Action("Indemnisation","GetLibelleByService")',  // "/Indemnisation/GetLibelleByService",
        type: 'POST',
        data: { id: id, titre: $("#Titre").val(), date: $("#DateModif").val() },
        success: function (url) {
            window.location.href = (url);
        }
    })
}

function OpenRapport(date, branche, service, equipe, nom) {
    //var url = "https://portal_bi.albingia.fr/ReportServer/Pages/ReportViewer.aspx?%2fProduction%2fIndemnisation_Comptage&rs:Command=Render" Prod url
    var url = "https://portal_bi.albingia.fr/ReportServer/Pages/ReportViewer.aspx?%2fTest%2fIndemnisation_Comptage&rs:Command=Render";
    url += "&DATE=" + date;
    url += "&BRANCHE=" + branche.toUpperCase();
    url += "&SERVICE=" + service.toUpperCase();
    url += "&EQUIPE=" + equipe.toUpperCase();
    url += "&COLLABORATEUR=" + nom.toUpperCase();
    window.open(url);
}