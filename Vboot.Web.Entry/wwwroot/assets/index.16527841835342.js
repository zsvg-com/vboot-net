import{_ as u,u as l,a as n,L as a}from"./index.1652784183534.js";import{a as _,aC as i,s as d,K as m,I as c,R as f,V as p,o as y,l as v}from"./vue.1652784183534.js";const L=_({name:"layout",components:{defaults:i(()=>n(()=>import("./defaults.1652784183534.js"),["assets/defaults.1652784183534.js","assets/vue.1652784183534.js","assets/index.1652784183534.js","assets/index.165278418353429.css","assets/aside.1652784183534.js","assets/main.1652784183534.js","assets/main.1652784183534.css","assets/logo-mini.1652784183534.js","assets/sortable.esm.1652784183534.js","assets/parent.1652784183534.js"])),classic:i(()=>n(()=>import("./classic.1652784183534.js"),["assets/classic.1652784183534.js","assets/vue.1652784183534.js","assets/index.1652784183534.js","assets/index.165278418353429.css","assets/aside.1652784183534.js","assets/main.1652784183534.js","assets/main.1652784183534.css","assets/logo-mini.1652784183534.js","assets/sortable.esm.1652784183534.js","assets/parent.1652784183534.js"])),transverse:i(()=>n(()=>import("./transverse.1652784183534.js"),["assets/transverse.1652784183534.js","assets/main.1652784183534.js","assets/main.1652784183534.css","assets/vue.1652784183534.js","assets/index.1652784183534.js","assets/index.165278418353429.css","assets/logo-mini.1652784183534.js","assets/sortable.esm.1652784183534.js","assets/parent.1652784183534.js"])),columns:i(()=>n(()=>import("./columns.1652784183534.js"),["assets/columns.1652784183534.js","assets/columns.1652784183534.css","assets/vue.1652784183534.js","assets/index.1652784183534.js","assets/index.165278418353429.css","assets/aside.1652784183534.js","assets/main.1652784183534.js","assets/main.1652784183534.css","assets/logo-mini.1652784183534.js","assets/sortable.esm.1652784183534.js","assets/parent.1652784183534.js"]))},setup(){const{proxy:o}=v(),r=l(),{themeConfig:e}=d(r),t=()=>{a.get("oldLayout")||a.set("oldLayout",e.value.layout);const s=document.body.clientWidth;s<1e3?(e.value.isCollapse=!1,o.mittBus.emit("layoutMobileResize",{layout:"defaults",clientWidth:s})):o.mittBus.emit("layoutMobileResize",{layout:a.get("oldLayout")?a.get("oldLayout"):e.value.layout,clientWidth:s})};return m(()=>{t(),window.addEventListener("resize",t)}),c(()=>{window.removeEventListener("resize",t)}),{themeConfig:e}}});function C(o,r,e,t,s,E){return y(),f(p(o.themeConfig.layout))}var h=u(L,[["render",C]]);export{h as default};