function showMessageModal(title, content, buttons) {
    $('#message-modal-title', $('#message-modal')).html(title);
    $('#message-modal-content', $('#message-modal')).html(content);
    $('#message-modal-footer', $('#message-modal')).html('');

    var button = '';

    if ((buttons) && (buttons != 'none') && (buttons.length > 0)) {
        _.each(buttons, function (values, key) {
            button = '<button type="button" class="btn ';

            if (values.class) {
                button += ' btn-' + values.class;
            } else {
                button += ' btn-white';
            }

            button += '"';

            if (values.dismiss == true) {
                button += ' data-dismiss="modal"';
            }

            button += '>';

            if (values.icon) {
                button += '<i class="fa fa-' + values.icon + '"></i>&nbsp; ';
            }

            button += values.label + '</button>';
            button = $(button);

            if (values.callback) {
                button.click(values.callback);
            }

            $('#message-modal-footer', $('#message-modal')).append(button);
        });
    } else {
        if (buttons != 'none') {
            button = '<a href="javascript:;" class="btn btn-primary" data-dismiss="modal">Close</a>';
        } else {
            button = '<a href="javascript:;" class="btn btn-primary disabled">Close</a>';
        }

        $('#message-modal-footer', $('#message-modal')).append(button);
    }

    $('#message-modal').modal({
        backdrop: 'static',
        keyboard: false
    });
    $('#message-modal').modal('show');
}

function hideMessageModal() {
    $('#message-modal').modal('hide');
}

function setCurrency(priceIndex) {
    $('#table > tbody  > tr').each(function () {
        var price = $(this).find('td:eq(' + priceIndex + ')').html();
        console.log(price);
        $(this).find('td:eq(' + priceIndex + ')').html('CAD$ ' + price );
    });
}  