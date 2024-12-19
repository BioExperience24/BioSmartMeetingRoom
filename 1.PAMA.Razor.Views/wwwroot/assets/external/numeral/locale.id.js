        numeral.register('locale', 'id', {
            delimiters: {
                thousands: '.',
                decimal:   ','
            },
            abbreviations: {
                thousand: ' Ribu',
                million:  ' Juta',
                billion:  ' Miliar',
                trillion: ' Triliun'
            },
            ordinal : function (number) {
                return number === 1 ? '' : '';
            },
            currency: {
                symbol: 'Rp '
            }
        });
        numeral.locale('id');