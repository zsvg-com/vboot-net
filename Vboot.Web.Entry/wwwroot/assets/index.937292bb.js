import{_ as B,af as k,k as h,a4 as g,h as b}from"./index.1f50be75.js";import{P as A}from"./index.4a6d20cf.js";import{A as T,ao as F,cl as E,r as v,a0 as r,B as D,a1 as j,a6 as t,w as e,H as w,D as P,ap as $,ae as a,J as y,ab as V}from"./vendor.8e08a5be.js";/* empty css                *//* empty css               *//* empty css                */import"./useWindowSizeFn.2016176c.js";import"./useContentViewHeight.78015564.js";const W=T({name:"TabsDemo",components:{CollapseContainer:k,PageWrapper:A,[F.name]:F,[E.name]:E},setup(){const u=h(),n=v(""),{closeAll:d,closeLeft:m,closeRight:C,closeOther:_,closeCurrent:i,refreshPage:s,setTitle:c}=g(),{createMessage:l}=b();function p(){n.value?c(n.value):l.error("\u8BF7\u8F93\u5165\u8981\u8BBE\u7F6E\u7684Tab\u6807\u9898\uFF01")}function o(f){u(`/feat/tabs/detail/${f}`)}return{closeAll:d,closeLeft:m,closeRight:C,closeOther:_,closeCurrent:i,toDetail:o,refreshPage:s,setTabTitle:p,title:n}}}),L={class:"mt-2 flex flex-grow-0"},N=a(" \u8BBE\u7F6ETab\u6807\u9898 "),H=a(" \u5173\u95ED\u6240\u6709 "),M=a(" \u5173\u95ED\u5DE6\u4FA7 "),O=a(" \u5173\u95ED\u53F3\u4FA7 "),R=a(" \u5173\u95ED\u5176\u4ED6 "),S=a(" \u5173\u95ED\u5F53\u524D "),x=a(" \u5237\u65B0\u5F53\u524D ");function z(u,n,d,m,C,_){const i=r("a-alert"),s=r("a-button"),c=r("a-input"),l=r("CollapseContainer"),p=r("PageWrapper");return D(),j(p,{title:"\u6807\u7B7E\u9875\u64CD\u4F5C\u793A\u4F8B"},{default:t(()=>[e(l,{title:"\u5728\u4E0B\u9762\u8F93\u5165\u6846\u8F93\u5165\u6587\u672C,\u5207\u6362\u540E\u56DE\u6765\u5185\u5BB9\u4F1A\u4FDD\u5B58"},{default:t(()=>[e(i,{banner:"",message:"\u8BE5\u64CD\u4F5C\u4E0D\u4F1A\u5F71\u54CD\u9875\u9762\u6807\u9898\uFF0C\u4EC5\u4FEE\u6539Tab\u6807\u9898"}),w("div",L,[e(s,{class:"mr-2",onClick:u.setTabTitle,type:"primary"},{default:t(()=>[N]),_:1},8,["onClick"]),e(c,{placeholder:"\u8BF7\u8F93\u5165",value:u.title,"onUpdate:value":n[0]||(n[0]=o=>u.title=o),class:"mr-4 w-12"},null,8,["value"])])]),_:1}),e(l,{class:"mt-4",title:"\u6807\u7B7E\u9875\u64CD\u4F5C"},{default:t(()=>[e(s,{class:"mr-2",onClick:u.closeAll},{default:t(()=>[H]),_:1},8,["onClick"]),e(s,{class:"mr-2",onClick:u.closeLeft},{default:t(()=>[M]),_:1},8,["onClick"]),e(s,{class:"mr-2",onClick:u.closeRight},{default:t(()=>[O]),_:1},8,["onClick"]),e(s,{class:"mr-2",onClick:u.closeOther},{default:t(()=>[R]),_:1},8,["onClick"]),e(s,{class:"mr-2",onClick:u.closeCurrent},{default:t(()=>[S]),_:1},8,["onClick"]),e(s,{class:"mr-2",onClick:u.refreshPage},{default:t(()=>[x]),_:1},8,["onClick"])]),_:1}),e(l,{class:"mt-4",title:"\u6807\u7B7E\u9875\u590D\u7528\u8D85\u51FA\u9650\u5236\u81EA\u52A8\u5173\u95ED(\u4F7F\u7528\u573A\u666F: \u52A8\u6001\u8DEF\u7531)"},{default:t(()=>[(D(),P(V,null,$(6,o=>e(s,{key:o,class:"mr-2",onClick:f=>u.toDetail(o)},{default:t(()=>[a(" \u6253\u5F00"+y(o)+"\u8BE6\u60C5\u9875 ",1)]),_:2},1032,["onClick"])),64))]),_:1})]),_:1})}var Y=B(W,[["render",z]]);export{Y as default};