var d=Object.defineProperty;var c=Object.getOwnPropertySymbols;var _=Object.prototype.hasOwnProperty,f=Object.prototype.propertyIsEnumerable;var m=(a,o,n)=>o in a?d(a,o,{enumerable:!0,configurable:!0,writable:!0,value:n}):a[o]=n,p=(a,o)=>{for(var n in o||(o={}))_.call(o,n)&&m(a,n,o[n]);if(c)for(var n of c(o))f.call(o,n)&&m(a,n,o[n]);return a};import{l as v}from"./logo-mini.1652784183534.js";import{_ as g}from"./index.1652784183534.js";import{a as F,a3 as b,ag as h,b as C,Z as e,S as l,a2 as u,o as E}from"./vue.1652784183534.js";const B=F({name:"makeSvgDemo",setup(){const a=b({tableData:[{a1:"name",a2:"svg \u56FE\u6807\u7EC4\u4EF6\u540D\u5B57 / svg \u8DEF\u5F84 url",a3:"string",a4:"",a5:""},{a1:"size",a2:"svg \u5927\u5C0F",a3:"number",a4:"",a5:14},{a1:"color",a2:"svg \u989C\u8272",a3:"string",a4:"",a5:""}]});return p({logoMini:v},h(a))}}),D={class:"svg-demo-container"};function z(a,o,n,k,w,A){const s=u("SvgIcon"),r=u("el-card"),t=u("el-table-column"),i=u("el-table");return E(),C("div",D,[e(r,{shadow:"hover",header:"svgIcon\uFF1A\u6F14\u793A\uFF08\u652F\u6301\u672C\u5730svg\uFF09"},{default:l(()=>[e(s,{name:"iconfont icon-shuju1",color:"red",size:30}),e(s,{name:"ele-Trophy",color:"var(--el-color-primary)",size:30}),e(s,{name:"fa fa-flag-checkered",color:"#09f",size:30}),e(s,{name:a.logoMini,color:"#09f",size:30},null,8,["name"])]),_:1}),e(r,{shadow:"hover",header:"svgIcon\uFF1A\u53C2\u6570",class:"mt15"},{default:l(()=>[e(i,{data:a.tableData,style:{width:"100%"}},{default:l(()=>[e(t,{prop:"a1",label:"\u53C2\u6570"}),e(t,{prop:"a2",label:"\u8BF4\u660E"}),e(t,{prop:"a3",label:"\u7C7B\u578B"}),e(t,{prop:"a4",label:"\u53EF\u9009\u503C"}),e(t,{prop:"a5",label:"\u9ED8\u8BA4\u503C"})]),_:1},8,["data"])]),_:1})])}var y=g(B,[["render",z]]);export{y as default};