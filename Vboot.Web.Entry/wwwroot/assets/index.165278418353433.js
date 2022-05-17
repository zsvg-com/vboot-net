var b=Object.defineProperty;var d=Object.getOwnPropertySymbols;var k=Object.prototype.hasOwnProperty,w=Object.prototype.propertyIsEnumerable;var f=(e,a,o)=>a in e?b(e,a,{enumerable:!0,configurable:!0,writable:!0,value:o}):e[a]=o,m=(e,a)=>{for(var o in a||(a={}))k.call(a,o)&&f(e,o,a[o]);if(d)for(var o of d(a))w.call(a,o)&&f(e,o,a[o]);return e};import{a as S,j as u,a3 as W,m as x,ag as T,n as _,a2 as $,T as B,U as I,o as i,b as s,e as p,M as O,W as h,Y as R,L as A,R as C,O as y}from"./vue.1652784183534.js";import{_ as N}from"./index.1652784183534.js";const z=S({name:"noticeBar",props:{mode:{type:String,default:()=>""},text:{type:String,default:()=>""},color:{type:String,default:()=>"var(--el-color-warning)"},background:{type:String,default:()=>"var(--el-color-warning-light-9)"},size:{type:[Number,String],default:()=>14},height:{type:Number,default:()=>40},delay:{type:Number,default:()=>1},speed:{type:Number,default:()=>100},scrollable:{type:Boolean,default:()=>!1},leftIcon:{type:String,default:()=>""},rightIcon:{type:String,default:()=>""}},setup(e,{emit:a}){const o=u(),n=u(),t=W({order:1,oneTime:0,twoTime:0,warpOWidth:0,textOWidth:0,isMode:!1}),l=()=>{_(()=>{t.warpOWidth=o.value.offsetWidth,t.textOWidth=n.value.offsetWidth,document.styleSheets[0].insertRule(`@keyframes oneAnimation {0% {left: 0px;} 100% {left: -${t.textOWidth}px;}}`),document.styleSheets[0].insertRule(`@keyframes twoAnimation {0% {left: ${t.warpOWidth}px;} 100% {left: -${t.textOWidth}px;}}`),r(),setTimeout(()=>{c()},e.delay*1e3)})},r=()=>{t.oneTime=t.textOWidth/e.speed,t.twoTime=(t.textOWidth+t.warpOWidth)/e.speed},c=()=>{t.order===1?(n.value.style.cssText=`animation: oneAnimation ${t.oneTime}s linear; opactity: 1;}`,t.order=2):n.value.style.cssText=`animation: twoAnimation ${t.twoTime}s linear infinite; opacity: 1;`},g=()=>{n.value.addEventListener("animationend",()=>{c()},!1)},v=()=>{if(!e.mode)return!1;e.mode==="closeable"?(t.isMode=!0,a("close")):e.mode==="link"&&a("link")};return x(()=>{if(e.scrollable)return!1;l(),g()}),m({noticeBarWarpRef:o,noticeBarTextRef:n,onRightIconClick:v},T(t))}}),M={class:"notice-bar-warp-text-box",ref:"noticeBarWarpRef"},D={key:1,class:"notice-bar-warp-slot"};function E(e,a,o,n,t,l){const r=$("SvgIcon");return B((i(),s("div",{class:"notice-bar",style:y({background:e.background,height:`${e.height}px`})},[p("div",{class:"notice-bar-warp",style:y({color:e.color,fontSize:`${e.size}px`})},[e.leftIcon?(i(),s("i",{key:0,class:O(["notice-bar-warp-left-icon",e.leftIcon])},null,2)):h("",!0),p("div",M,[e.scrollable?(i(),s("div",D,[A(e.$slots,"default",{},void 0,!0)])):(i(),s("div",{key:0,class:"notice-bar-warp-text",ref:"noticeBarTextRef"},R(e.text),513))],512),e.rightIcon?(i(),C(r,{key:1,name:e.rightIcon,class:"notice-bar-warp-right-icon",onClick:e.onRightIconClick},null,8,["name","onClick"])):h("",!0)],4)],4)),[[I,!e.isMode]])}var U=N(z,[["render",E],["__scopeId","data-v-ae559b32"]]);export{U as N};
