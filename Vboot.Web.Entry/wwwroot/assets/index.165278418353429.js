var T=Object.defineProperty;var S=Object.getOwnPropertySymbols;var I=Object.prototype.hasOwnProperty,L=Object.prototype.propertyIsEnumerable;var O=(E,g,p)=>g in E?T(E,g,{enumerable:!0,configurable:!0,writable:!0,value:p}):E[g]=p,w=(E,g)=>{for(var p in g||(g={}))I.call(g,p)&&O(E,p,g[p]);if(S)for(var p of S(g))L.call(g,p)&&O(E,p,g[p]);return E};import{a8 as D,_ as B}from"./index.1652784183534.js";import{a as R,a3 as U,ag as W,b as F,Z as P,S as C,a2 as M,o as H,X as N}from"./vue.1652784183534.js";var A={exports:{}};(function(E,g){(function(f,n){E.exports=n()})(window,function(){return function(p){var f={};function n(r){if(f[r])return f[r].exports;var s=f[r]={i:r,l:!1,exports:{}};return p[r].call(s.exports,s,s.exports,n),s.l=!0,s.exports}return n.m=p,n.c=f,n.d=function(r,s,d){n.o(r,s)||Object.defineProperty(r,s,{enumerable:!0,get:d})},n.r=function(r){typeof Symbol!="undefined"&&Symbol.toStringTag&&Object.defineProperty(r,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(r,"__esModule",{value:!0})},n.t=function(r,s){if(s&1&&(r=n(r)),s&8||s&4&&typeof r=="object"&&r&&r.__esModule)return r;var d=Object.create(null);if(n.r(d),Object.defineProperty(d,"default",{enumerable:!0,value:r}),s&2&&typeof r!="string")for(var b in r)n.d(d,b,function(i){return r[i]}.bind(null,b));return d},n.n=function(r){var s=r&&r.__esModule?function(){return r.default}:function(){return r};return n.d(s,"a",s),s},n.o=function(r,s){return Object.prototype.hasOwnProperty.call(r,s)},n.p="",n(n.s=0)}({"./src/index.js":function(p,f,n){n.r(f),n("./src/sass/index.scss");var r=n("./src/js/init.js"),s=r.default.init;typeof window!="undefined"&&(window.printJS=s),f.default=s},"./src/js/browser.js":function(p,f,n){n.r(f);var r={isFirefox:function(){return typeof InstallTrigger!="undefined"},isIE:function(){return navigator.userAgent.indexOf("MSIE")!==-1||!!document.documentMode},isEdge:function(){return!r.isIE()&&!!window.StyleMedia},isChrome:function(){var d=arguments.length>0&&arguments[0]!==void 0?arguments[0]:window;return!!d.chrome},isSafari:function(){return Object.prototype.toString.call(window.HTMLElement).indexOf("Constructor")>0||navigator.userAgent.toLowerCase().indexOf("safari")!==-1},isIOSChrome:function(){return navigator.userAgent.toLowerCase().indexOf("crios")!==-1}};f.default=r},"./src/js/functions.js":function(p,f,n){n.r(f),n.d(f,"addWrapper",function(){return b}),n.d(f,"capitalizePrint",function(){return i}),n.d(f,"collectStyles",function(){return l}),n.d(f,"addHeader",function(){return e}),n.d(f,"cleanUp",function(){return u}),n.d(f,"isRawHTML",function(){return y});var r=n("./src/js/modal.js"),s=n("./src/js/browser.js");function d(t){return typeof Symbol=="function"&&typeof Symbol.iterator=="symbol"?d=function(c){return typeof c}:d=function(c){return c&&typeof Symbol=="function"&&c.constructor===Symbol&&c!==Symbol.prototype?"symbol":typeof c},d(t)}function b(t,a){var c="font-family:"+a.font+" !important; font-size: "+a.font_size+" !important; width:100%;";return'<div style="'+c+'">'+t+"</div>"}function i(t){return t.charAt(0).toUpperCase()+t.slice(1)}function l(t,a){for(var c=document.defaultView||window,h="",m=c.getComputedStyle(t,""),v=0;v<m.length;v++)(a.targetStyles.indexOf("*")!==-1||a.targetStyle.indexOf(m[v])!==-1||o(a.targetStyles,m[v]))&&m.getPropertyValue(m[v])&&(h+=m[v]+":"+m.getPropertyValue(m[v])+";");return h+="max-width: "+a.maxWidth+"px !important; font-size: "+a.font_size+" !important;",h}function o(t,a){for(var c=0;c<t.length;c++)if(d(a)==="object"&&a.indexOf(t[c])!==-1)return!0;return!1}function e(t,a){var c=document.createElement("div");if(y(a.header))c.innerHTML=a.header;else{var h=document.createElement("h1"),m=document.createTextNode(a.header);h.appendChild(m),h.setAttribute("style",a.headerStyle),c.appendChild(h)}t.insertBefore(c,t.childNodes[0])}function u(t){t.showModal&&r.default.close(),t.onLoadingEnd&&t.onLoadingEnd(),(t.showModal||t.onLoadingStart)&&window.URL.revokeObjectURL(t.printable);var a="mouseover";(s.default.isChrome()||s.default.isFirefox())&&(a="focus");var c=function h(){window.removeEventListener(a,h),t.onPrintDialogClose();var m=document.getElementById(t.frameId);m&&m.remove()};window.addEventListener(a,c)}function y(t){var a=new RegExp("<([A-Za-z][A-Za-z0-9]*)\\b[^>]*>(.*?)</\\1>");return a.test(t)}},"./src/js/html.js":function(p,f,n){n.r(f);var r=n("./src/js/functions.js"),s=n("./src/js/print.js");function d(l){return typeof Symbol=="function"&&typeof Symbol.iterator=="symbol"?d=function(e){return typeof e}:d=function(e){return e&&typeof Symbol=="function"&&e.constructor===Symbol&&e!==Symbol.prototype?"symbol":typeof e},d(l)}f.default={print:function(o,e){var u=i(o.printable)?o.printable:document.getElementById(o.printable);if(!u){window.console.error("Invalid HTML element id: "+o.printable);return}o.printableElement=b(u,o),o.header&&Object(r.addHeader)(o.printableElement,o),s.default.send(o,e)}};function b(l,o){for(var e=l.cloneNode(),u=Array.prototype.slice.call(l.childNodes),y=0;y<u.length;y++)if(o.ignoreElements.indexOf(u[y].id)===-1){var t=b(u[y],o);e.appendChild(t)}switch(o.scanStyles&&l.nodeType===1&&e.setAttribute("style",Object(r.collectStyles)(l,o)),l.tagName){case"SELECT":e.value=l.value;break;case"CANVAS":e.getContext("2d").drawImage(l,0,0);break}return e}function i(l){return d(l)==="object"&&l&&(l instanceof HTMLElement||l.nodeType===1)}},"./src/js/image.js":function(p,f,n){n.r(f);var r=n("./src/js/functions.js"),s=n("./src/js/print.js"),d=n("./src/js/browser.js");f.default={print:function(i,l){i.printable.constructor!==Array&&(i.printable=[i.printable]),i.printableElement=document.createElement("div"),i.printable.forEach(function(o){var e=document.createElement("img");if(e.setAttribute("style",i.imageStyle),e.src=o,d.default.isFirefox()){var u=e.src;e.src=u}var y=document.createElement("div");y.appendChild(e),i.printableElement.appendChild(y)}),i.header&&Object(r.addHeader)(i.printableElement,i),s.default.send(i,l)}}},"./src/js/init.js":function(p,f,n){n.r(f);var r=n("./src/js/browser.js"),s=n("./src/js/modal.js"),d=n("./src/js/pdf.js"),b=n("./src/js/html.js"),i=n("./src/js/raw-html.js"),l=n("./src/js/image.js"),o=n("./src/js/json.js");function e(y){return typeof Symbol=="function"&&typeof Symbol.iterator=="symbol"?e=function(a){return typeof a}:e=function(a){return a&&typeof Symbol=="function"&&a.constructor===Symbol&&a!==Symbol.prototype?"symbol":typeof a},e(y)}var u=["pdf","html","image","json","raw-html"];f.default={init:function(){var t={printable:null,fallbackPrintable:null,type:"pdf",header:null,headerStyle:"font-weight: 300;",maxWidth:800,properties:null,gridHeaderStyle:"font-weight: bold; padding: 5px; border: 1px solid #dddddd;",gridStyle:"border: 1px solid lightgray; margin-bottom: -1px;",showModal:!1,onError:function(x){throw x},onLoadingStart:null,onLoadingEnd:null,onPrintDialogClose:function(){},onIncompatibleBrowser:function(){},modalMessage:"Retrieving Document...",frameId:"printJS",printableElement:null,documentTitle:"Document",targetStyle:["clear","display","width","min-width","height","min-height","max-height"],targetStyles:["border","box","break","text-decoration"],ignoreElements:[],repeatTableHeader:!0,css:null,style:null,scanStyles:!0,base64:!1,onPdfOpen:null,font:"TimesNewRoman",font_size:"12pt",honorMarginPadding:!0,honorColor:!1,imageStyle:"max-width: 100%;"},a=arguments[0];if(a===void 0)throw new Error("printJS expects at least 1 attribute.");switch(e(a)){case"string":t.printable=encodeURI(a),t.fallbackPrintable=t.printable,t.type=arguments[1]||t.type;break;case"object":t.printable=a.printable,t.fallbackPrintable=typeof a.fallbackPrintable!="undefined"?a.fallbackPrintable:t.printable,t.fallbackPrintable=t.base64?"data:application/pdf;base64,".concat(t.fallbackPrintable):t.fallbackPrintable;for(var c in t)c==="printable"||c==="fallbackPrintable"||(t[c]=typeof a[c]!="undefined"?a[c]:t[c]);break;default:throw new Error('Unexpected argument type! Expected "string" or "object", got '+e(a))}if(!t.printable)throw new Error("Missing printable information.");if(!t.type||typeof t.type!="string"||u.indexOf(t.type.toLowerCase())===-1)throw new Error("Invalid print type. Available types are: pdf, html, image and json.");t.showModal&&s.default.show(t),t.onLoadingStart&&t.onLoadingStart();var h=document.getElementById(t.frameId);h&&h.parentNode.removeChild(h);var m=document.createElement("iframe");switch(r.default.isFirefox()?m.setAttribute("style","width: 1px; height: 100px; position: fixed; left: 0; top: 0; opacity: 0; border-width: 0; margin: 0; padding: 0"):m.setAttribute("style","visibility: hidden; height: 0; width: 0; position: absolute; border: 0"),m.setAttribute("id",t.frameId),t.type!=="pdf"&&(m.srcdoc="<html><head><title>"+t.documentTitle+"</title>",t.css&&(Array.isArray(t.css)||(t.css=[t.css]),t.css.forEach(function(j){m.srcdoc+='<link rel="stylesheet" href="'+j+'">'})),m.srcdoc+="</head><body></body></html>"),t.type){case"pdf":if(r.default.isIE())try{console.info("Print.js doesn't support PDF printing in Internet Explorer.");var v=window.open(t.fallbackPrintable,"_blank");v.focus(),t.onIncompatibleBrowser()}catch(j){t.onError(j)}finally{t.showModal&&s.default.close(),t.onLoadingEnd&&t.onLoadingEnd()}else d.default.print(t,m);break;case"image":l.default.print(t,m);break;case"html":b.default.print(t,m);break;case"raw-html":i.default.print(t,m);break;case"json":o.default.print(t,m);break}}}},"./src/js/json.js":function(p,f,n){n.r(f);var r=n("./src/js/functions.js"),s=n("./src/js/print.js");function d(i){return typeof Symbol=="function"&&typeof Symbol.iterator=="symbol"?d=function(o){return typeof o}:d=function(o){return o&&typeof Symbol=="function"&&o.constructor===Symbol&&o!==Symbol.prototype?"symbol":typeof o},d(i)}f.default={print:function(l,o){if(d(l.printable)!=="object")throw new Error("Invalid javascript data object (JSON).");if(typeof l.repeatTableHeader!="boolean")throw new Error("Invalid value for repeatTableHeader attribute (JSON).");if(!l.properties||!Array.isArray(l.properties))throw new Error("Invalid properties array for your JSON data.");l.properties=l.properties.map(function(e){return{field:d(e)==="object"?e.field:e,displayName:d(e)==="object"?e.displayName:e,columnSize:d(e)==="object"&&e.columnSize?e.columnSize+";":100/l.properties.length+"%;"}}),l.printableElement=document.createElement("div"),l.header&&Object(r.addHeader)(l.printableElement,l),l.printableElement.innerHTML+=b(l),s.default.send(l,o)}};function b(i){var l=i.printable,o=i.properties,e='<table style="border-collapse: collapse; width: 100%;">';i.repeatTableHeader&&(e+="<thead>"),e+="<tr>";for(var u=0;u<o.length;u++)e+='<th style="width:'+o[u].columnSize+";"+i.gridHeaderStyle+'">'+Object(r.capitalizePrint)(o[u].displayName)+"</th>";e+="</tr>",i.repeatTableHeader&&(e+="</thead>"),e+="<tbody>";for(var y=0;y<l.length;y++){e+="<tr>";for(var t=0;t<o.length;t++){var a=l[y],c=o[t].field.split(".");if(c.length>1)for(var h=0;h<c.length;h++)a=a[c[h]];else a=a[o[t].field];e+='<td style="width:'+o[t].columnSize+i.gridStyle+'">'+a+"</td>"}e+="</tr>"}return e+="</tbody></table>",e}},"./src/js/modal.js":function(p,f,n){n.r(f);var r={show:function(d){var b="font-family:sans-serif; display:table; text-align:center; font-weight:300; font-size:30px; left:0; top:0;position:fixed; z-index: 9990;color: #0460B5; width: 100%; height: 100%; background-color:rgba(255,255,255,.9);transition: opacity .3s ease;",i=document.createElement("div");i.setAttribute("style",b),i.setAttribute("id","printJS-Modal");var l=document.createElement("div");l.setAttribute("style","display:table-cell; vertical-align:middle; padding-bottom:100px;");var o=document.createElement("div");o.setAttribute("class","printClose"),o.setAttribute("id","printClose"),l.appendChild(o);var e=document.createElement("span");e.setAttribute("class","printSpinner"),l.appendChild(e);var u=document.createTextNode(d.modalMessage);l.appendChild(u),i.appendChild(l),document.getElementsByTagName("body")[0].appendChild(i),document.getElementById("printClose").addEventListener("click",function(){r.close()})},close:function(){var d=document.getElementById("printJS-Modal");d&&d.parentNode.removeChild(d)}};f.default=r},"./src/js/pdf.js":function(p,f,n){n.r(f);var r=n("./src/js/print.js"),s=n("./src/js/functions.js");f.default={print:function(i,l){if(i.base64){var o=Uint8Array.from(atob(i.printable),function(u){return u.charCodeAt(0)});d(i,l,o);return}i.printable=/^(blob|http|\/\/)/i.test(i.printable)?i.printable:window.location.origin+(i.printable.charAt(0)!=="/"?"/"+i.printable:i.printable);var e=new window.XMLHttpRequest;e.responseType="arraybuffer",e.addEventListener("error",function(){Object(s.cleanUp)(i),i.onError(e.statusText,e)}),e.addEventListener("load",function(){if([200,201].indexOf(e.status)===-1){Object(s.cleanUp)(i),i.onError(e.statusText,e);return}d(i,l,e.response)}),e.open("GET",i.printable,!0),e.send()}};function d(b,i,l){var o=new window.Blob([l],{type:"application/pdf"});o=window.URL.createObjectURL(o),i.setAttribute("src",o),r.default.send(b,i)}},"./src/js/print.js":function(p,f,n){n.r(f);var r=n("./src/js/browser.js"),s=n("./src/js/functions.js"),d={send:function(e,u){document.getElementsByTagName("body")[0].appendChild(u);var y=document.getElementById(e.frameId);y.onload=function(){if(e.type==="pdf"){r.default.isFirefox()?setTimeout(function(){return b(y,e)},1e3):b(y,e);return}var t=y.contentWindow||y.contentDocument;if(t.document&&(t=t.document),t.body.appendChild(e.printableElement),e.type!=="pdf"&&e.style){var a=document.createElement("style");a.innerHTML=e.style,t.head.appendChild(a)}var c=t.getElementsByTagName("img");c.length>0?i(Array.from(c)).then(function(){return b(y,e)}):b(y,e)}}};function b(o,e){try{if(o.focus(),r.default.isEdge()||r.default.isIE())try{o.contentWindow.document.execCommand("print",!1,null)}catch{o.contentWindow.print()}else o.contentWindow.print()}catch(u){e.onError(u)}finally{r.default.isFirefox()&&(o.style.visibility="hidden",o.style.left="-1px"),Object(s.cleanUp)(e)}}function i(o){var e=o.map(function(u){if(u.src&&u.src!==window.location.href)return l(u)});return Promise.all(e)}function l(o){return new Promise(function(e){var u=function y(){!o||typeof o.naturalWidth=="undefined"||o.naturalWidth===0||!o.complete?setTimeout(y,500):e()};u()})}f.default=d},"./src/js/raw-html.js":function(p,f,n){n.r(f);var r=n("./src/js/print.js");f.default={print:function(d,b){d.printableElement=document.createElement("div"),d.printableElement.setAttribute("style","width:100%"),d.printableElement.innerHTML=d.printable,r.default.send(d,b)}}},"./src/sass/index.scss":function(p,f,n){},0:function(p,f,n){p.exports=n("./src/index.js")}}).default})})(A);var K=D(A.exports);const z=R({name:"funPrintJs",setup(){const E=U({});return w({onPrintJs:()=>{K({printable:"printRef",type:"html",css:["//at.alicdn.com/t/font_2298093_o73r8wjdhlg.css","//unpkg.com/element-plus/dist/index.css"],scanStyles:!1,style:"@media print{.mb15{margin-bottom:15px;}.el-button--small i.iconfont{font-size: 12px !important;margin-right: 5px;}}"})}},W(E))}}),J={id:"printRef"},_=N(" \u70B9\u51FB\u6253\u5370\u6F14\u793A ");function V(E,g,p,f,n,r){const s=M("el-alert"),d=M("SvgIcon"),b=M("el-button"),i=M("el-card");return H(),F("div",J,[P(i,{shadow:"hover",header:"\u6253\u5370\u6F14\u793A"},{default:C(()=>[P(s,{title:"\u611F\u8C22\u4F18\u79C0\u7684 `print-js`\uFF0C\u9879\u76EE\u5730\u5740\uFF1Ahttps://github.com/crabbly/Print.js\u3002\u8BF7\u5728\u6253\u5370\u5F39\u7A97 `\u66F4\u591A\u8BBE\u7F6E` \u4E2D\u5F00\u542F `\u80CC\u666F\u56FE\u5F62\u3002`",type:"success",closable:!1,class:"mb15"}),P(b,{onClick:E.onPrintJs,size:"default",type:"primary"},{default:C(()=>[P(d,{name:"iconfont icon-dayin"}),_]),_:1},8,["onClick"])]),_:1})])}var G=B(z,[["render",V]]);export{G as default};
