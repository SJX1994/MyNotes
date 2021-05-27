var c_tabs = {
	data() {
		return {
			IsShow: true,
			sel: 0,
			TabInfos: undefined
			/* [
			         {label:"MAX",content:"Max"},
			         {label:"MAYA",content:"Maya"},
			         {label:"SPP",content:"<li>1111</li><li>2222</li>"}
			       ] */
		}
	},
	methods: {
		select(item, index) {
			this.sel = index;
		},
		Update(data) {
			if (data != undefined && data.Tabs != undefined) {
				this.TabInfos = data.Tabs;
				this.IsShow = true;
			} else {
				this.IsShow = false;
			}
		}
	},
	mounted() {
		if (this.TabInfos == undefined) {
			this.IsShow = false;
		}
	},
	template: `
	<div class="tab" :v-show="IsShow">
						<div class="tabHeader">
							<ul>
								<li v-for="(t,i) in TabInfos" @click="select(t,i)" :class="{ 'active': sel===i }" >
									{{t.label.toUpperCase()}}
								</li>
							</ul>
						</div>
						<div class="tabContent" v-for="(t,i) in TabInfos" v-show="sel===i" v-html="t.content">
						</div>
					</div>
	`
}
