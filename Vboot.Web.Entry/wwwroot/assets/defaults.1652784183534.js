import{a as y,aB as H,s as C,B as x,k as h,a2 as o,o as t,R as s,S as r,Z as a,W as d,M as m,l as F}from"./vue.1652784183534.js";import{_ as $,u as g}from"./index.1652784183534.js";import{A as M}from"./aside.1652784183534.js";import{H as R,M as w}from"./main.1652784183534.js";import"./logo-mini.1652784183534.js";import"./sortable.esm.1652784183534.js";import"./parent.1652784183534.js";const A=y({name:"layoutDefaults",components:{Aside:M,Header:R,Main:w},setup(){const{proxy:e}=F(),n=H(),c=g(),{themeConfig:l}=C(c),i=x(()=>l.value.isFixedHeader);return h(()=>n.path,()=>{e.$refs.layoutDefaultsScrollbarRef.wrap$.scrollTop=0}),{isFixedHeader:i}}});function B(e,n,c,l,i,T){const f=o("Aside"),p=o("Header"),_=o("Main"),b=o("el-scrollbar"),u=o("el-container"),k=o("el-backtop");return t(),s(u,{class:"layout-container"},{default:r(()=>[a(f),a(u,{class:m(["flex-center",{"layout-backtop":!e.isFixedHeader}])},{default:r(()=>[e.isFixedHeader?(t(),s(p,{key:0})):d("",!0),a(b,{ref:"layoutDefaultsScrollbarRef",class:m({"layout-backtop":e.isFixedHeader})},{default:r(()=>[e.isFixedHeader?d("",!0):(t(),s(p,{key:0})),a(_)]),_:1},8,["class"])]),_:1},8,["class"]),a(k,{target:".layout-backtop .el-scrollbar__wrap"})]),_:1})}var W=$(A,[["render",B]]);export{W as default};