import{A as D,cl as w,cm as B,aR as x,r as g,a0 as a,B as j,a1 as _,a6 as s,w as o,al as h,$ as R,ae as t}from"./vendor.8e08a5be.js";/* empty css                *//* empty css                */import{u as m}from"./index.7c24f3ba.js";import b from"./Modal1.18ae7554.js";import y from"./Modal2.648cb3d8.js";import k from"./Modal3.9d987ec4.js";import v from"./Modal4.f1e1488a.js";import{P as $}from"./index.4a6d20cf.js";import{_ as T}from"./index.1f50be75.js";import"./useWindowSizeFn.2016176c.js";import"./BasicForm.4740433f.js";/* empty css               *//* empty css               *//* empty css               *//* empty css                *//* empty css              *//* empty css               *//* empty css                *//* empty css                */import"./index.4765eeb8.js";/* empty css                *//* empty css                *//* empty css                */import"./uuid.2b29000c.js";import"./download.1020b52a.js";import"./base64Conver.08b9f4ec.js";import"./index.b2563f35.js";import"./useForm.6f5945bb.js";/* empty css               *//* empty css                */import"./useContentViewHeight.78015564.js";const V=D({components:{Alert:w,Modal1:b,Modal2:y,Modal3:k,Modal4:v,PageWrapper:$,ASpace:B},setup(){const e=x(null),[u,{openModal:c}]=m(),[M,{openModal:E}]=m(),[C,{openModal:i}]=m(),[n,{openModal:l}]=m(),d=g(!1),p=g(null);function F(){l(!0,{data:"content",info:"Info"})}function A(){c(!0)}function f(r){switch(r){case 1:e.value=b;break;case 2:e.value=y;break;case 3:e.value=k;break;default:e.value=v;break}R(()=>{p.value={data:Math.random(),info:"Info222"},d.value=!0})}return{register1:u,openModal1:c,register2:M,openModal2:E,register3:C,openModal3:i,register4:n,openModal4:l,modalVisible:d,userData:p,openTargetModal:f,send:F,currentModal:e,openModalLoading:A}}}),P=t(" \u6253\u5F00\u5F39\u7A97,\u52A0\u8F7D\u52A8\u6001\u6570\u636E\u5E76\u81EA\u52A8\u8C03\u6574\u9AD8\u5EA6(\u9ED8\u8BA4\u53EF\u4EE5\u62D6\u52A8/\u5168\u5C4F) "),W=t(" \u6253\u5F00\u5F39\u7A97 "),S=t(" \u6253\u5F00\u5F39\u7A97 "),H=t(" \u6253\u5F00\u5F39\u7A97\u5E76\u4F20\u9012\u6570\u636E "),I=t(" \u6253\u5F00\u5F39\u7A971 "),L=t(" \u6253\u5F00\u5F39\u7A972 "),N=t(" \u6253\u5F00\u5F39\u7A973 "),z=t(" \u6253\u5F00\u5F39\u7A974 ");function U(e,u,c,M,E,C){const i=a("Alert"),n=a("a-button"),l=a("a-space"),d=a("Modal1"),p=a("Modal2"),F=a("Modal3"),A=a("Modal4"),f=a("PageWrapper");return j(),_(f,{title:"modal\u7EC4\u4EF6\u4F7F\u7528\u793A\u4F8B"},{default:s(()=>[o(i,{message:`\u4F7F\u7528 useModal \u8FDB\u884C\u5F39\u7A97\u64CD\u4F5C\uFF0C\u9ED8\u8BA4\u53EF\u4EE5\u62D6\u52A8\uFF0C\u53EF\u4EE5\u901A\u8FC7 draggable\r
    \u53C2\u6570\u8FDB\u884C\u63A7\u5236\u662F\u5426\u53EF\u4EE5\u62D6\u52A8/\u5168\u5C4F\uFF0C\u5E76\u6F14\u793A\u4E86\u5728Modal\u5185\u52A8\u6001\u52A0\u8F7D\u5185\u5BB9\u5E76\u81EA\u52A8\u8C03\u6574\u9AD8\u5EA6`,"show-icon":""}),o(n,{type:"primary",class:"my-4",onClick:e.openModalLoading},{default:s(()=>[P]),_:1},8,["onClick"]),o(i,{message:"\u5185\u5916\u540C\u65F6\u540C\u65F6\u663E\u793A\u9690\u85CF","show-icon":""}),o(n,{type:"primary",class:"my-4",onClick:e.openModal2},{default:s(()=>[W]),_:1},8,["onClick"]),o(i,{message:"\u81EA\u9002\u5E94\u9AD8\u5EA6","show-icon":""}),o(n,{type:"primary",class:"my-4",onClick:e.openModal3},{default:s(()=>[S]),_:1},8,["onClick"]),o(i,{message:"\u5185\u5916\u6570\u636E\u4EA4\u4E92","show-icon":""}),o(n,{type:"primary",class:"my-4",onClick:e.send},{default:s(()=>[H]),_:1},8,["onClick"]),o(i,{message:"\u4F7F\u7528\u52A8\u6001\u7EC4\u4EF6\u7684\u65B9\u5F0F\u5728\u9875\u9762\u5185\u4F7F\u7528\u591A\u4E2A\u5F39\u7A97","show-icon":""}),o(l,null,{default:s(()=>[o(n,{type:"primary",class:"my-4",onClick:u[0]||(u[0]=r=>e.openTargetModal(1))},{default:s(()=>[I]),_:1}),o(n,{type:"primary",class:"my-4",onClick:u[1]||(u[1]=r=>e.openTargetModal(2))},{default:s(()=>[L]),_:1}),o(n,{type:"primary",class:"my-4",onClick:u[2]||(u[2]=r=>e.openTargetModal(3))},{default:s(()=>[N]),_:1}),o(n,{type:"primary",class:"my-4",onClick:u[3]||(u[3]=r=>e.openTargetModal(4))},{default:s(()=>[z]),_:1})]),_:1}),(j(),_(h(e.currentModal),{visible:e.modalVisible,"onUpdate:visible":u[4]||(u[4]=r=>e.modalVisible=r),userData:e.userData},null,8,["visible","userData"])),o(d,{onRegister:e.register1,minHeight:100},null,8,["onRegister"]),o(p,{onRegister:e.register2},null,8,["onRegister"]),o(F,{onRegister:e.register3},null,8,["onRegister"]),o(A,{onRegister:e.register4},null,8,["onRegister"])]),_:1})}var be=T(V,[["render",U]]);export{be as default};