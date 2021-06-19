var ajax = {
	request:function(method,url,callback){
		var req = new XMLHttpRequest();
		req.onreadystatechange = function(){
			if(req.readyState == 4 && req.status == 200)
			{
				callback(req.responseText);
			}
		};
		
		req.open(method,url,true);
		req.send();
	}
}