import{A as _,c3 as S,r as n,a0 as s,B as i,a1 as r,a6 as o,H as l,w as c,al as m,F as f,G as T,ae as x}from"./vendor.8e08a5be.js";import{P as b}from"./index.4a6d20cf.js";import{_ as X,aV as h,aW as R,aX as w,aY as Y,aZ as C,a_ as j,a$ as y,b0 as B,b1 as E,b2 as F,b3 as $,b4 as g,b5 as k}from"./index.1f50be75.js";/* empty css               *//* empty css                */import"./useWindowSizeFn.2016176c.js";import"./useContentViewHeight.78015564.js";const V=["Fade","Scale","SlideY","ScrollY","SlideYReverse","ScrollYReverse","SlideX","ScrollX","SlideXReverse","ScrollXReverse","ScaleRotate","ExpandX","Expand"],W=V.map(e=>({label:e,value:e,key:e})),A=_({components:{Select:S,PageWrapper:b,FadeTransition:h,ScaleTransition:R,SlideYTransition:w,ScrollYTransition:Y,SlideYReverseTransition:C,ScrollYReverseTransition:j,SlideXTransition:y,ScrollXTransition:B,SlideXReverseTransition:E,ScrollXReverseTransition:F,ScaleRotateTransition:$,ExpandXTransition:g,ExpandTransition:k},setup(){const e=n("Fade"),a=n(!0);function t(){a.value=!1,setTimeout(()=>{a.value=!0},300)}return{options:W,value:e,start:t,show:a}}}),P={class:"flex"},N=x(" start "),D={class:"box"};function H(e,a,t,z,G,I){const d=s("Select"),p=s("a-button"),u=s("PageWrapper");return i(),r(u,{title:"\u52A8\u753B\u7EC4\u4EF6\u793A\u4F8B"},{default:o(()=>[l("div",P,[c(d,{options:e.options,value:e.value,"onUpdate:value":a[0]||(a[0]=v=>e.value=v),placeholder:"\u9009\u62E9\u52A8\u753B",style:{width:"150px"}},null,8,["options","value"]),c(p,{type:"primary",class:"ml-4",onClick:e.start},{default:o(()=>[N]),_:1},8,["onClick"])]),(i(),r(m(`${e.value}Transition`),null,{default:o(()=>[f(l("div",D,null,512),[[T,e.show]])]),_:1}))]),_:1})}var O=X(A,[["render",H],["__scopeId","data-v-191e0ae4"]]);export{O as default};