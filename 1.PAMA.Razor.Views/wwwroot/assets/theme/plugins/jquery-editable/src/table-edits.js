
;(function ($, window, document, undefined) {
    var pluginName = "editable",
        defaults = {
            keyboard: true,
            dblclick: true,
            button: true,
            buttonSelector: ".edit",
            maintainWidth: true,
            dropdowns: {},
            edit: function() {},
            save: function() {},
            cancel: function() {}
        };

    function editable(element, options) {
        this.element = element;
        this.options = $.extend({}, defaults, options) ;

        this._defaults = defaults;
        this._name = pluginName;

        this.init();
    }

    editable.prototype = {
        init: function() {
            this.editing = false;

            if (this.options.dblclick) {
                $(this.element)
                    .css('cursor', 'pointer')
                    .bind('dblclick', this.toggle.bind(this));
            }

            if (this.options.button) {
                $(this.options.buttonSelector, this.element)
                    .bind('click', this.toggle.bind(this));
            }
        },

        toggle: function(e) {
            e.preventDefault();

            this.editing = !this.editing;

            if (this.editing) {
                this.edit();
            } else {
                this.save();
            }
        },

        edit: function() {
            var instance = this,
                values = {};

            $('td[data-field]', this.element).each(function() {
                var input,
                    field = $(this).data('field'),
                    value = $(this).text(),
                    width = $(this).width();

                values[field] = value;

                $(this).empty();

                if (instance.options.maintainWidth) {
                    $(this).width(width);
                }

                if (field in instance.options.dropdowns) {
                    input = $('<select data-obj="select" data-item="'+field+'" ></select>');
                    var selecttype = "";
                    var selectVAL = "";
                    for (var i = 0; i < instance.options.dropdowns[field].length; i++) {
                        if( typeof(instance.options.dropdowns[field][i]) == "object" ){
                            selecttype = "object";
                            if(value == instance.options.dropdowns[field][i]['name'] ){
                                selectVAL = instance.options.dropdowns[field][i]['id'];
                                $('<option selected value="'+instance.options.dropdowns[field][i]['id']+'"></option>')
                                 .text(instance.options.dropdowns[field][i]['name'])
                                 .appendTo(input);
                            }else{
                                $('<option value="'+instance.options.dropdowns[field][i]['id']+'"></option>')
                                 .text(instance.options.dropdowns[field][i]['name'])
                                 .appendTo(input);
                            }
                        }else{
                            selecttype = "string";
                            $('<option></option>')
                             .text(instance.options.dropdowns[field][i])
                             .appendTo(input);
                        }
                        
                    };
                    var dsa = value
                    if(selecttype == "object"){
                        dsa = selectVAL;
                    }
                    // console.log(value, selecttype, instance._captureEvent);
                    input.val(dsa)
                         .data('old-value', dsa)
                         .dblclick(instance._captureEvent);
                } else {
                    input = $('<input type="text" />')
                        .val(value)
                        .data('old-value', value)
                        .dblclick(instance._captureEvent);
                }

                input.appendTo(this);

                if (instance.options.keyboard) {
                    input.keydown(instance._captureKey.bind(instance));
                }
            });

            this.options.edit.bind(this.element)(values);
        },

        save: function() {
            var instance = this,
                values = {};

            $('td[data-field]', this.element).each(function() {
                var value = $(':input', this).val();
                var valuetext = value;
                var ht = $(this).html();
                var check = $(ht).attr('data-obj');
                var checkitem = $(ht).attr('data-item');
                if(check == "select"){
                    // console.log(instance.options.dropdowns[checkitem]);
                    if(instance.options.dropdowns[checkitem].length > 0){
                        var ro =  instance.options.dropdowns[checkitem];
                        for (var x in ro){
                            if(typeof(ro[x]) == 'object'){
                                if(value == ro[x]['id']){
                                    valuetext = ro[x]['name'];
                                    break;
                                }
                            }else{
                                if(value == ro[x]){
                                    valuetext = ro[x];
                                    break;
                                }
                            }
                        }   
                    }else{
                        valuetext = value;
                    }
                    
                }
                values[$(this).data('field')] = value;

                $(this).empty()
                       .text(valuetext);
            });

            this.options.save.bind(this.element)(values);
        },

        cancel: function() {
            var instance = this,
                values = {};

            $('td[data-field]', this.element).each(function() {
                var value = $(':input', this).data('old-value');
                var valuetext = value;
                var ht = $(this).html();
                var check = $(ht).attr('data-obj');
                var checkitem = $(ht).attr('data-item');
                if(check == "select"){
                    // console.log(instance.options.dropdowns[checkitem]);
                    if(instance.options.dropdowns[checkitem].length > 0){
                        var ro =  instance.options.dropdowns[checkitem];
                        for (var x in ro){
                            if(typeof(ro[x]) == 'object'){
                                if(value == ro[x]['id']){
                                    valuetext = ro[x]['name'];
                                    break;
                                }
                            }else{
                                if(value == ro[x]){
                                    valuetext = ro[x];
                                    break;
                                }
                            }
                        }   
                    }else{
                        valuetext = value;
                    }
                    
                }
                values[$(this).data('field')] = value;

                $(this).empty()
                       .text(valuetext);
            });

            this.options.cancel.bind(this.element)(values);
        },

        _captureEvent: function(e) {
            e.stopPropagation();
        },

        _captureKey: function(e) {
            if (e.which === 13) {
                this.editing = false;
                this.save();
            } else if (e.which === 27) {
                this.editing = false;
                this.cancel();
            }
        }
    };

    $.fn[pluginName] = function(options) {
        return this.each(function () {
            if (!$.data(this, "plugin_" + pluginName)) {
                $.data(this, "plugin_" + pluginName,
                new editable(this, options));
            }
        });
    };

})(jQuery, window, document);
