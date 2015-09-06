var PracticeWidget = {
    originalWord: $("#original-word"),
    input1: $("#input1"),
    input2: $("#input2"),
    input3: $("#input3"),
    input4: $("#input4"),
    input5: $("#input5"),
    form: $("#main-form"),
    nextButton: $("#next-button"),
    allInputs: {},

    init: function() {
        this.bindUIActions();
        PracticeWidget.allInputs = PracticeWidget.form.find(':input');
        PracticeWidget.input1.focus();
    },

    bindUIActions: function() {
        this.input1.keyup(PracticeWidget.checkInput);
        this.input2.keyup(PracticeWidget.checkInput);
        this.input3.keyup(PracticeWidget.checkInput);
        this.input4.keyup(PracticeWidget.checkInput);
        this.input5.keyup(PracticeWidget.checkInput);
    },

    checkInput: function() {
        if (this.value.toUpperCase() === PracticeWidget.originalWord.val().toUpperCase()) {
            this.disabled = true;
            $(this).closest('.form-group').addClass('has-success has-feedback');
            $(this).after('<span class="glyphicon glyphicon-ok form-control-feedback" aria-hidden="true"></span><span id="inputSuccess2Status" class="sr-only">(success)</span>');
            PracticeWidget.moveFocusToNextInput(this);
        }

        if (PracticeWidget.isAllInputsDisabled()) {
            PracticeWidget.nextButton.prop("disabled", false);
            PracticeWidget.nextButton.focus();
        }
    },

    moveFocusToNextInput : function(currentInput) {
        PracticeWidget.allInputs.eq(PracticeWidget.allInputs.index(currentInput) + 1).focus();
    },

    isAllInputsDisabled : function() {
        return PracticeWidget.input1.prop("disabled") 
            && PracticeWidget.input2.prop("disabled")
            && PracticeWidget.input3.prop("disabled")
            && PracticeWidget.input4.prop("disabled")
            && PracticeWidget.input5.prop("disabled");
    }
};