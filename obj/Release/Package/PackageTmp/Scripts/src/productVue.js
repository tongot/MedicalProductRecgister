
new Vue({
    el: '#app',
    data: () => ({
        state:'',
        modalHeader: 'Add Ingredient',
        isEdit: false,
        btnEnable: false,
        btnDelete:
        {
            disabled: false,
            content: 'delete'
        },
        btnEdit:
        {
            disabled: false,
            content: 'edit'
        },
        btnText: 'Add',
        btnEngr: 'view ingredients',
        alerts: [],
        commonName: '',
        INCIName: '',
        Quantity: '',
        Unit: '',
        Restricted: '',
        ingredientId: 0,
        ingredients: [],
        hasError: false,
        index: ''
    }),
    methods: {
        getState(condition) {
            return condition=='YES'?'alert-danger':''
        },
        validate() {
            this.hasError = false;
            if (!this.commonName) {
                this.alerts.push('common name is requierd')
                this.hasError = true
            }
            if (!this.INCIName) {
                this.alerts.push('INCI Name is requierd')
                this.hasError = true
            }
            if (this.Quantity <= 0) {
                this.alerts.push('Quantity must be greater than zero')
                this.hasError = true
            }
            if (!this.Unit) {
                this.alerts.push('select unit')
                this.hasError = true
            }
            if (!this.Restricted) {
                this.alerts.push('select restriction')
                this.hasError = true
            }
        },
        editIngredient(id) {
            this.alerts = [];
            this.validate();
            if (this.hasError == false) {
                this.btnEdit.disabled = true
                this.btnEdit.content = 'Editing...'
                this.alerts = [];
                axios.post('/Ingredients/Edit/', {
                    IngredientsId: this.ingredientId,
                    CommonName: this.commonName,
                    INCIName: this.INCIName,
                    Quantity: this.Quantity,
                    Unit: this.Unit,
                    ProductId: id,
                    Restricted: this.Restricted == 'YES' ? true : false
                }).then((response) => {
                    if (response.status === 200) {
                        this.alerts.push("Ingredient edited :)");
                        this.ingredients[this.index] = response.data
                    }
                    this.btnEdit.disabled = false
                    this.btnEdit.content = 'Edit'
                }, e => {
                    console.log(e.response);
                    this.btnEdit.disabled = false
                    this.btnEdit.content = 'Edit'
                })
            }

        },
        postIngredient(id) {
            this.alerts = [];
            this.validate();
            if (this.hasError == false) {
                this.btnEnable = true
                this.btnText = 'Adding...'
                this.alerts = [];
                axios.post('/Ingredients/Create/', {

                    CommonName: this.commonName,
                    INCIName: this.INCIName,
                    Quantity: this.Quantity,
                    Unit: this.Unit,
                    ProductId: id,
                    Restricted: this.Restricted == 'YES' ? true : false
                }).then((response) => {
                    if (response.status === 200) {
                        this.alerts.push("Ingredient added :)");
                        this.ingredients.push(response.data)
                        this.clearValues();
                    }
                    this.btnEnable = false
                    this.btnText = 'Add'
                }, e => {
                    console.log(e.response);
                    this.btnEnable = false
                    this.btnText = 'Add'
                })
            }

        },
        getUnit(unit) {
            if (unit == 0) {
                return 'Grams'
            } else if (unit == 1) {
                return 'Milligrams'
            }
            else if (unit == 2) {
                return 'Litre'
            }
            else {
                return 'Milliliters'
            }

        },
        clearValues() {
            this.commonName = ''
            this.INCIName = ''
            this.Quantity = ''
            this.Unit = ''
            this.Restricted = '';
        },
        addModal() {
            this.alerts=[]
            this.clearValues();
            this.isEdit = false;
            $('#modalIngredient').modal('show');
        },
        getEditItem(ingredient, index) {
            this.modalHeader = 'Edit Ingredient';
            this.alerts = [];
            this.index = index;
            this.isEdit = true;
            this.ingredientId = ingredient.IngredientsId
            this.commonName = ingredient.CommonName;
            this.INCIName = ingredient.INCIName;
            this.Quantity = ingredient.Quantity;
            this.Unit = this.getUnit(ingredient.Unit);
            this.Restricted = ingredient.Restricted == true ? 'YES' : 'NO';
            $('#modalIngredient').modal('show');
        },
        getIngredients(id) {
            this.btnEngr = "getting ingredients..."
            axios.get('/Ingredients/Index?productId=' + id).then((response) => {
                this.ingredients = response.data
                this.btnEngr = "ingredients"
            })
        },
        deleteIngrediant(id, index) {
            this.btnDelete.content = 'deleting'
            this.btnDelete.disabled = true
            axios.post('/Ingredients/Delete/' + id).then(() => {
                this.btnDelete.content = 'delete'
                this.btnDelete.disabled = false
                this.ingredients.splice(index, 1)
            })
        }
    }, mounted() {
        var url = window.location.href.split('/')
        this.getIngredients(url[url.length - 1])
    }
})