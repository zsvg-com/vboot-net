import{b9 as w,ba as x,bb as W,bc as g,_ as v,af as y}from"./index.1f50be75.js";import{r as b,aR as $,u as c,ak as j,ai as R,A as B,a0 as C,B as I,a1 as P,a6 as d,w as f,ae as k}from"./vendor.8e08a5be.js";import{P as S}from"./index.4a6d20cf.js";/* empty css               *//* empty css                */import"./useWindowSizeFn.2016176c.js";import"./useContentViewHeight.78015564.js";const z=Symbol("watermark-dom");function M(n=b(document.body)){const a=W(function(){const t=c(n);if(!t)return;const{clientHeight:e,clientWidth:o}=t;i({height:e,width:o})}),s=z.toString(),l=$(),m=()=>{const t=c(l);l.value=void 0;const e=c(n);!e||(t&&e.removeChild(t),w(e,a))};function _(t){const e=document.createElement("canvas"),o=300,u=240;Object.assign(e,{width:o,height:u});const r=e.getContext("2d");return r&&(r.rotate(-20*Math.PI/120),r.font="15px Vedana",r.fillStyle="rgba(0, 0, 0, 0.15)",r.textAlign="left",r.textBaseline="middle",r.fillText(t,o/20,u)),e.toDataURL("image/png")}function i(t={}){const e=c(l);!e||(g(t.width)&&(e.style.width=`${t.width}px`),g(t.height)&&(e.style.height=`${t.height}px`),g(t.str)&&(e.style.background=`url(${_(t.str)}) left top repeat`))}const p=t=>{if(c(l))return i({str:t}),s;const e=document.createElement("div");l.value=e,e.id=s,e.style.pointerEvents="none",e.style.top="0px",e.style.left="0px",e.style.position="absolute",e.style.zIndex="100000";const o=c(n);if(!o)return s;const{clientHeight:u,clientWidth:r}=o;return i({str:t,width:r,height:u}),o.appendChild(e),s};function h(t){p(t),x(document.documentElement,a),R()&&j(()=>{m()})}return{setWatermark:h,clear:m}}const V=B({components:{CollapseContainer:y,PageWrapper:S},setup(){const n=b(null),{setWatermark:a,clear:s}=M();return{setWatermark:a,clear:s,areaRef:n}}}),A=k(" Create "),E=k(" Clear "),H=k(" Reset ");function L(n,a,s,l,m,_){const i=C("a-button"),p=C("CollapseContainer"),h=C("PageWrapper");return I(),P(h,{title:"\u6C34\u5370\u793A\u4F8B"},{default:d(()=>[f(p,{class:"w-full h-32 bg-white rounded-md",title:"Global WaterMark"},{default:d(()=>[f(i,{type:"primary",class:"mr-2",onClick:a[0]||(a[0]=t=>n.setWatermark("WaterMark Info"))},{default:d(()=>[A]),_:1}),f(i,{color:"error",class:"mr-2",onClick:n.clear},{default:d(()=>[E]),_:1},8,["onClick"]),f(i,{color:"warning",class:"mr-2",onClick:a[1]||(a[1]=t=>n.setWatermark("WaterMark Info New"))},{default:d(()=>[H]),_:1})]),_:1})]),_:1})}var q=v(V,[["render",L]]);export{q as default};
