var v=Object.defineProperty;var i=Object.getOwnPropertySymbols;var w=Object.prototype.hasOwnProperty,g=Object.prototype.propertyIsEnumerable;var l=(e,s,t)=>s in e?v(e,s,{enumerable:!0,configurable:!0,writable:!0,value:t}):e[s]=t,_=(e,s)=>{for(var t in s||(s={}))w.call(s,t)&&l(e,t,s[t]);if(i)for(var t of i(s))g.call(s,t)&&l(e,t,s[t]);return e};import{i as x}from"./getStyleSheets.1652784183534.js";import{_ as F}from"./index.1652784183534.js";import{a as I,a3 as y,m as A,ag as B,b as d,Z as u,S as a,a2 as n,o as c,F as L,a7 as $,R as k,e as o,M as C,Y as E}from"./vue.1652784183534.js";const S=I({name:"pagesAwesome",setup(){const e=y({sheetsIconList:[]}),s=()=>{x.awe().then(t=>e.sheetsIconList=t)};return A(()=>{s()}),_({},B(e))}}),b={class:"awesome-container"},D={class:"iconfont-warp"},M={class:"flex-margin"},N={class:"iconfont-warp-value"},R={class:"iconfont-warp-label mt10"};function V(e,s,t,z,G,Y){const m=n("el-col"),p=n("el-row"),f=n("el-card");return c(),d("div",b,[u(f,{shadow:"hover",header:`fontawesome \u5B57\u4F53\u56FE\u6807(\u81EA\u52A8\u8F7D\u5165)\uFF1A${e.sheetsIconList.length-24}\u4E2A`},{default:a(()=>[u(p,{class:"iconfont-row"},{default:a(()=>[(c(!0),d(L,null,$(e.sheetsIconList,(r,h)=>(c(),k(m,{xs:12,sm:8,md:6,lg:4,xl:2,key:h},{default:a(()=>[o("div",D,[o("div",M,[o("div",N,[o("i",{class:C([r,"fa"])},null,2)]),o("div",R,E(r),1)])])]),_:2},1024))),128))]),_:1})]),_:1},8,["header"])])}var J=F(S,[["render",V],["__scopeId","data-v-6a551862"]]);export{J as default};