'use strict';

class CellEdit {
    constructor() {
        this.cell = null;
    }

    init(cellElement) {
        this.cell = $(cellElement);
        let val = this.cell.html();

        if (this.cell.find("input").length == 0) {
            let code = '<input type="text" id="_edit" value="' + val + '" class="form-control w-100 col-12"/>';
            this.cell.empty().append(code);
            this.cell.addClass('editable-cell');
            $('#_edit').focus();
            let el = document.getElementById('_edit');
            el.setSelectionRange(el.value.length, el.value.length);
            this.changeFocus();
        }
    }

    changeFocus() {
        let self = this;
        $('#_edit').change(function (e) {
            let val = $(this).val();
            //let td = $(this).parent();

            self.cell.removeClass('editable-cell');

            if (!isNumber(val)) {
                this.resultDangerHighLiter(self.cell);
                e.preventDefault();
            } else {
                self.saveData(self.cell, val)
                    .catch(() => {
                        e.preventDefault();
                    });
            }
        });
    }

    resultDangerHighLiter(obj) {
        setErrorHighlight($(obj));
        showError('В числовом поле не допускается ввод символов или букв');
    }

    setErrorHighlight(obj) {
        setTimeout(() => {
            $(obj).addClass('table-danger');
        }, 2000);

        setTimeout(() => {
            $(obj).removeClass('table-danger');
        }, 10000);
    }

    saveData(el, val) { return new Promise(); };
}
