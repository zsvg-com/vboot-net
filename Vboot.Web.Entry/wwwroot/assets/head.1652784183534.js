var r=Object.defineProperty;var o=Object.getOwnPropertySymbols;var u=Object.prototype.hasOwnProperty,_=Object.prototype.propertyIsEnumerable;var n=(e,t,a)=>t in e?r(e,t,{enumerable:!0,configurable:!0,writable:!0,value:a}):e[t]=a,c=(e,t)=>{for(var a in t||(t={}))u.call(t,a)&&n(e,a,t[a]);if(o)for(var a of o(t))_.call(t,a)&&n(e,a,t[a]);return e};import{_ as p,H as i}from"./index.1652784183534.js";import{a as m,a3 as l,K as f,I as v,ag as h,b as Y,e as s,Y as Q,az as S,aA as w,o as x}from"./vue.1652784183534.js";const H=m({name:"chartHead",setup(){const e=l({time:{txt:"",fun:0}}),t=()=>{e.time.txt=i(new Date,"YYYY-mm-dd HH:MM:SS WWW QQQQ"),e.time.fun=window.setInterval(()=>{e.time.txt=i(new Date,"YYYY-mm-dd HH:MM:SS WWW QQQQ")},1e3)};return f(()=>{t()}),v(()=>{window.clearInterval(e.time.fun)}),c({},h(e))}}),d=e=>(S("data-v-a791c862"),e=e(),w(),e),I={class:"big-data-up mb15"},W={class:"up-left"},B=d(()=>s("i",{class:"el-icon-time mr5"},null,-1)),D=d(()=>s("div",{class:"up-center"},[s("span",null,"\u667A\u6167\u519C\u4E1A\u7CFB\u7EDF\u5E73\u53F0")],-1));function M(e,t,a,g,C,E){return x(),Y("div",I,[s("div",W,[B,s("span",null,Q(e.time.txt),1)]),D])}var A=p(H,[["render",M],["__scopeId","data-v-a791c862"]]);export{A as default};