var R=Object.defineProperty;var l=Object.getOwnPropertySymbols;var _=Object.prototype.hasOwnProperty,V=Object.prototype.propertyIsEnumerable;var c=(s,e,t)=>e in s?R(s,e,{enumerable:!0,configurable:!0,writable:!0,value:t}):s[e]=t,m=(s,e)=>{for(var t in e||(e={}))_.call(e,t)&&c(s,t,e[t]);if(l)for(var t of l(e))V.call(e,t)&&c(s,t,e[t]);return s};import{a as k,aB as y,s as f,a3 as N,B as T,K as A,n as B,I as C,k as K,ag as g,b as L,Z as p,S as v,l as P,a2 as $,o as r,_ as x,R as d,V as I,aE as E}from"./vue.1652784183534.js";import{_ as U,c as b,u as D}from"./index.1652784183534.js";const M=k({name:"layoutParentView",setup(){const{proxy:s}=P(),e=y(),t=b(),i=D(),{keepAliveNames:o}=f(t),{themeConfig:u}=f(i),a=N({refreshRouterViewKey:null,keepAliveNameList:[]}),n=T(()=>u.value.animation);return A(()=>{a.keepAliveNameList=o.value,s.mittBus.on("onTagsViewRefreshRouterView",h=>{a.keepAliveNameList=o.value.filter(w=>e.name!==w),a.refreshRouterViewKey=null,B(()=>{a.refreshRouterViewKey=h,a.keepAliveNameList=o.value})})}),C(()=>{s.mittBus.off("onTagsViewRefreshRouterView")}),K(()=>e.fullPath,()=>{a.refreshRouterViewKey=decodeURI(e.fullPath)}),m({setTransitionName:n},g(a))}}),S={class:"h100"};function Z(s,e,t,i,o,u){const a=$("router-view");return r(),L("div",S,[p(a,null,{default:v(({Component:n})=>[p(x,{name:s.setTransitionName,mode:"out-in"},{default:v(()=>[(r(),d(E,{include:s.keepAliveNameList},[(r(),d(I(n),{key:s.refreshRouterViewKey,class:"w100"}))],1032,["include"]))]),_:2},1032,["name"])]),_:1})])}var F=U(M,[["render",Z]]);export{F as default};
