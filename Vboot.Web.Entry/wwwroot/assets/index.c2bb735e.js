var q=Object.defineProperty,z=Object.defineProperties;var G=Object.getOwnPropertyDescriptors;var C=Object.getOwnPropertySymbols;var W=Object.prototype.hasOwnProperty,I=Object.prototype.propertyIsEnumerable;var $=(r,o,e)=>o in r?q(r,o,{enumerable:!0,configurable:!0,writable:!0,value:e}):r[o]=e,S=(r,o)=>{for(var e in o||(o={}))W.call(o,e)&&$(r,e,o[e]);if(C)for(var e of C(o))I.call(o,e)&&$(r,e,o[e]);return r},T=(r,o)=>z(r,G(o));var V=(r,o,e)=>new Promise((v,t)=>{var x=p=>{try{_(e.next(p))}catch(s){t(s)}},g=p=>{try{_(e.throw(p))}catch(s){t(s)}},_=p=>p.done?v(p.value):Promise.resolve(p.value).then(x,g);_((e=e.apply(r,o)).next())});import{P as J}from"./index.4a6d20cf.js";import{_ as L}from"./index.fa31d1d6.js";import{ah as k,_ as K}from"./index.1f50be75.js";import{A as b,r as U,_ as Q,a0 as f,B,D as R,w as a,P as D,j as X,a6 as i,u as n,bu as Y,ae as E,J as y,a4 as M,ab as Z,t as ee,a1 as te,H as O}from"./vendor.8e08a5be.js";import{u as ae,B as ne,a as oe}from"./index.b3a760e0.js";import{u as le}from"./index.7c24f3ba.js";import{O as re}from"./OrgModal.821e8224.js";import{h as ie}from"./edit.6fd2a5d5.js";import{c as ue,a as se,d as de,e as me}from"./list.20bccedc.js";/* empty css               *//* empty css                */import"./useWindowSizeFn.2016176c.js";import"./useContentViewHeight.78015564.js";import"./useContextMenu.6d6ea44d.js";/* empty css               *//* empty css               *//* empty css               *//* empty css               *//* empty css              */const pe=b({name:"DeptTree",components:{BasicTree:L},emits:["select"],setup(r,{emit:o}){const e=U([]);function v(){return V(this,null,function*(){e.value=yield k.get({url:"/gen/org/dept/tree"})})}function t(x,g){x.length>0&&o("select",{id:x[0],name:g.selectedNodes[0].name})}return Q(()=>{v()}),{treeData:e,handleSelect:t}}}),ce={class:"m-4 mr-0 overflow-hidden bg-white"};function fe(r,o,e,v,t,x){const g=f("BasicTree");return B(),R("div",ce,[a(g,{title:"\u90E8\u95E8\u5217\u8868",toolbar:"",search:"",clickRowToExpand:!1,treeData:r.treeData,fieldNames:{key:"id",title:"name"},onSelect:r.handleSelect},null,8,["treeData","onSelect"])])}var _e=K(pe,[["render",fe]]);const xe=b({emits:["success","register"],setup(r,{emit:o}){const[e,{openModal:v}]=le(),t=D({loadingText:"",url:"/sys/org/dept",data:{},rules:{name:[{required:!0,message:"\u8BF7\u8F93\u5165\u540D\u79F0"}],type:[{required:!0,message:"\u8BF7\u9009\u62E9\u7C7B\u578B"}]}}),[x,{setDrawerProps:g,changeLoading:_,closeDrawer:p}]=ae(u=>V(this,null,function*(){t.loadingText="\u52A0\u8F7D\u4E2D",_(!0),u.record.id?t.data=yield k.get({url:t.url+"/one/"+u.record.id}):(t.data=u.record,t.data.parent&&t.data.parent.id===""&&(t.data.parent=null),t.data.type=2,t.data.avtag=!0),_(!1)})),s=X(()=>t.data.parent?t.data.parent.name:"");function h(){v(!0,{opener:"parent",orgType:2,selectMode:1,orgs:t.data.parent&&t.data.parent.id?[ee(t.data.parent)]:[]})}function F(u){u.opener=="parent"&&(t.data.parent=u.orgs[0]),console.log(u)}return(u,w)=>{const d=f("vxe-input"),c=f("vxe-form-item"),j=f("vxe-radio-button"),A=f("vxe-radio-group"),N=f("vxe-switch"),P=f("vxe-textarea"),H=f("vxe-form");return B(),R(Z,null,[a(n(ne),M(u.$attrs,{loadingText:n(t).loadingText,showFooter:"",title:"\u90E8\u95E8\u4FE1\u606F",width:"70%",onOk:w[1]||(w[1]=l=>n(ie)(u.$refs.xForm,n(t),o,n(g),n(_),n(p))),onRegister:n(x)}),{default:i(()=>[a(H,{"title-colon":"",ref:(l,m)=>{m.xForm=l},"title-align":"right","title-width":"100",data:n(t).data,rules:n(t).rules},{default:i(()=>[a(c,{title:"\u90E8\u95E8\u540D\u79F0",field:"name","item-render":{},span:"12","title-width":"150"},{default:i(({data:l})=>[a(d,{modelValue:l.name,"onUpdate:modelValue":m=>l.name=m,clearable:""},null,8,["modelValue","onUpdate:modelValue"])]),_:1}),a(c,{title:"\u4E0A\u7EA7\u90E8\u95E8",field:"parent","item-render":{},span:"12","title-width":"150"},{default:i(({data:l})=>[a(d,{modelValue:n(s),"onUpdate:modelValue":w[0]||(w[0]=m=>Y(s)?s.value=m:null),"suffix-icon":"vxe-icon--search",readonly:"",onClick:h},null,8,["modelValue"])]),_:1}),a(c,{title:"\u90E8\u95E8\u7C7B\u578B",field:"type","item-render":{},span:"12","title-width":"150"},{default:i(({data:l})=>[a(A,{modelValue:l.type,"onUpdate:modelValue":m=>l.type=m},{default:i(()=>[a(j,{label:1,content:"\u516C\u53F8"}),a(j,{label:2,content:"\u90E8\u95E8"})]),_:2},1032,["modelValue","onUpdate:modelValue"])]),_:1}),a(c,{title:"\u662F\u5426\u53EF\u7528",field:"avtag","item-render":{},span:"12","title-width":"150"},{default:i(({data:l})=>[a(N,{modelValue:l.avtag,"onUpdate:modelValue":m=>l.avtag=m,"open-label":"\u662F","close-label":"\u5426"},null,8,["modelValue","onUpdate:modelValue"])]),_:1}),a(c,{title:"\u6392\u5E8F\u53F7",field:"ornum","item-render":{},span:"12","title-width":"150"},{default:i(({data:l})=>[a(d,{modelValue:l.ornum,"onUpdate:modelValue":m=>l.ornum=m,type:"number"},null,8,["modelValue","onUpdate:modelValue"])]),_:1}),a(c,{title:"\u90E8\u95E8\u5907\u6CE8",field:"notes","item-render":{},span:"24","title-width":"150"},{default:i(({data:l})=>[a(P,{modelValue:l.notes,"onUpdate:modelValue":m=>l.notes=m,placeholder:"\u8BF7\u8F93\u5165\u5907\u6CE8",autosize:{minRows:4,maxRows:6},clearable:""},null,8,["modelValue","onUpdate:modelValue"])]),_:1}),a(c,{title:"\u521B\u5EFA\u65F6\u95F4",field:"crtim","item-render":{},span:"12","title-width":"150"},{default:i(({data:l})=>[E(y(l.crtim),1)]),_:1}),a(c,{title:"\u66F4\u65B0\u65F6\u95F4",field:"uptim","item-render":{},span:"12","title-width":"150"},{default:i(({data:l})=>[E(y(l.uptim),1)]),_:1})]),_:1},8,["data","rules"])]),_:1},16,["loadingText","onRegister"]),a(re,{onRegister:n(e),onCloseModal:F},null,8,["onRegister"])],64)}}}),ge={class:"w-3/4 xl:w-4/5 ztable"},ve=E("\u67E5 \u8BE2"),he=E("\u65B0 \u589E"),we=E("\u5220 \u9664"),Ee=["onClick"],Fe={name:"SysOrgDept",inheritAttrs:!1,customOptions:{}};function Ve(r){const o=U({}),e=D({name:"",pid:"",pname:""}),v=D(ue("/sys/org/dept",{},e,[{type:"checkbox",align:"center",width:42,fixed:"left"},{type:"seq",align:"center",width:50,fixed:"left"},{field:"name",title:"\u90E8\u95E8\u540D\u79F0",width:300,fixed:"left",slots:{default:"name_default"}},{field:"notes",title:"\u5907\u6CE8"},{field:"crtim",title:"\u521B\u5EFA\u65F6\u95F4",width:140},{field:"uptim",title:"\u66F4\u65B0\u65F6\u95F4",width:140}])),t=()=>{o.value.commitProxy("query")};function x(s){s!=null?(e.pid=s.id,e.pname=s.name):(e.pid="",e.pname=""),t(),console.log(e)}const[g,{openDrawer:_}]=oe();function p(){t()}return(s,h)=>{const F=f("vxe-input"),u=f("vxe-button"),w=f("vxe-grid");return B(),te(n(J),{dense:"",contentFullHeight:"",fixedHeight:"",contentClass:"flex"},{default:i(()=>[a(_e,{class:"w-1/4 xl:w-1/5",onSelect:x,style:{margin:"6px 0 6px 6px"}}),O("div",ge,[a(w,M({ref:(d,c)=>{c.xGrid=d,o.value=d}},n(v)),{tbtns:i(()=>[a(F,{modelValue:n(e).name,"onUpdate:modelValue":h[0]||(h[0]=d=>n(e).name=d),placeholder:"\u8F93\u5165\u540D\u79F0\u67E5\u8BE2"},null,8,["modelValue"]),a(u,{onClick:t,status:"primary"},{default:i(()=>[ve]),_:1}),a(u,{onClick:h[1]||(h[1]=d=>n(se)({parent:{id:n(e).pid,name:n(e).pname}},n(_)))},{default:i(()=>[he]),_:1}),a(u,{onClick:h[2]||(h[2]=d=>n(de)(s.$refs.xGrid))},{default:i(()=>[we]),_:1})]),name_default:i(({row:d})=>[O("span",{style:{cursor:"pointer",color:"#3e9ece"},onClick:c=>n(me)({id:d.id},n(_))},y(d.name),9,Ee)]),_:1},16)]),a(xe,{onRegister:n(g),onSuccess:p},null,8,["onRegister"])]),_:1})}}const Ge=b(T(S({},Fe),{setup:Ve}));export{Ge as default};