//Our Scripts will be here

$('#ListUserBelongToTheTeam a').click(function (e) {
    e.preventDefault()
    $(this).tab('show')
})

$('#ListUserNOTBelongToTheTeam a').click(function (e) {
    e.preventDefault()
    $(this).tab('show')
})