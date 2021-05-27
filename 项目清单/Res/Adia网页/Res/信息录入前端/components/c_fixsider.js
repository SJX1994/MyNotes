var c_fixsider = 	{
	data() {return{
		Tags: undefined,
		Projects: undefined,
		Sources: undefined,
		IsShowItems: [false, false, false],
		self: undefined
	}},
	methods: {
		Update(data) {
			if (data.Tags != undefined) {
				this.IsShowItems[0] = true;
				this.Tags = data.Tags;
			} else {
				this.IsShowItems[0] = false;
			}

			if (data.Projects != undefined) {
				this.IsShowItems[1] = true;
				this.Projects = data.Projects;
			} else {
				this.IsShowItems[1] = false;
			}

			if (data.Sources != undefined) {
				this.IsShowItems[2] = true;
				this.Sources = data.Sources;
			} else {
				this.IsShowItems[2] = false;
			}

		},
		siderBtnClick(e) {
			if (this.self.offsetLeft == 0) {
				this.self.style['margin-left'] = -this.self.clientWidth + "px"
			} else {
				this.self.style['margin-left'] = "0px"
			}
		}
	},
	
	mounted() {
		this.self = document.getElementById("siderContent");
	},
	props: ['isCanHide'],
	template:`
	<aside class="sider" id="siderContent">
	<button class="siderBtn" id="siderBtn" v-show="isCanHide" @click="siderBtnClick">◆</button>
	<h1 v-if="IsShowItems[0]">TAG:</h1>
	<ul>
		<li v-for="t in Tags">
			<a :href="'../?sk='+t">{{t.charAt(0).toUpperCase()+t.slice(1)}}</a>
		</li>
	</ul>
	
	<h1 v-if="IsShowItems[1]">Project:</h1>
	<ul>
		<li v-for="p in Projects">
			<a href="#">{{p}}</a>
		</li>
	</ul>
	
	<h1 v-if="IsShowItems[2]">Source:</h1>
	<ul>
		<li v-for="s in Sources">
			<a href="#">{{s}}</a>
		</li>
	</ul>
	</aside>
	`
}