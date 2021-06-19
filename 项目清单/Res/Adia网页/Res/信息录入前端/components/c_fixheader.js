var c_fixheader = {
	data() {
		return {
			userInfo: {
				name: "UserName",
				IP: "0.0.0.0",
				group: "",
				level: 1
			},
			self: undefined
		}
	},

	mounted() {
		this.self = document.getElementById("fixedheader");
	},
	template: `
	<div class="container">
		<div class="logo">
			<h1><a :href="'http://'+location.host">Logo</a></h1>
		</div>
	
		<div class="userPanel">
			<h1>{{userInfo.name}}</h1>
			<h2>{{userInfo.IP}}</h2>
		</div>
	
		<div class="clearfix">
		</div>
	</div>
	`
}
