
var c_siderrightcontent = {
	data() {
		return {
			AssetID: "",
			ImgSrc: "",
			assets: undefined,
			btnDownload: "立即下载",
			FileInfo:undefined,
		}
	},
	components: {
		'tabs': c_tabs
	},
	methods: {
		Update(data) {
			if (data.AssetID != undefined) {
				this.AssetID = data.AssetID;
			}

			if (data.ImgSrc != undefined) {
				this.ImgSrc = data.ImgSrc;
			}

			this.assets = data.assets;
		},

		TryDownload() {
			this.assets.forEach(function(item, index) {
				window.open(item.src);
			});
		},
	},
	
	mounted() {
		this.FileInfo = this.$refs.tabinfo;
	},
	
	props: ['isCanHide'],
	template: `
	<div class="siderRightContent" id="siderRightContent">
				<h1>{{AssetID}}</h1>
				<div style="display:inline-block; vertical-align:top;">
					<div class="imgframe">
						<div id="imgContanier">
							<img v-bind:src="ImgSrc" />
						</div>
					</div>
				</div>
				
								<div style="display:inline-block; vertical-align:top;">
									<tabs ref="tabinfo"></tabs>
								</div>
		
				<div class="downloadSetting" id="downloadSetting">
					<label v-for="f in assets">
						<input type="checkbox" checked="f.isSelected" @change="function(e){f.isSelected=e.target.checked;}" />
						{{f.filename}}({{(f.size/1048576.0).toFixed(2)}}MB)
					</label>
				</div>
				<button class="downloadTryBtn" @click="TryDownload">{{btnDownload}}</button>
				</div>
	`
}
