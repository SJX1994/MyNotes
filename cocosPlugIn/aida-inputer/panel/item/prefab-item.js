let fs = require('fire-fs');
let packageName = "resource-cleanup";

module.exports = {
    init() {
        Vue.component('prefab-item', {
            props: ['data', 'index'],
            template: fs.readFileSync(Editor.url('packages://' + packageName + '/panel/item/prefab-item.html', 'utf8')) + "",
            created() {

            },
            methods: {
                onBtnClickDel(){
                    window.plugin.onBtnClickDel(this.data);
                },
            },
            computed: {},
        });
    }
};