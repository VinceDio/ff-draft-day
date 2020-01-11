$(document).ready(function () {
    $('.player-list').select2({
        placeholder: 'Select Player',
        minimumInputLength: 2,
        selectOnClose: true,
        allowClear: true,
        tags: false,
        ajax: {
            url: '/Players/PlayerList',
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return { searchText: params.term };
            },
            processResults: function (data) {
                return { results: data }
            }
        }
    });
});
