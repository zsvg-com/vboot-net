var S=Object.defineProperty;var b=Object.getOwnPropertySymbols;var x=Object.prototype.hasOwnProperty,y=Object.prototype.propertyIsEnumerable;var E=(e,l,a)=>l in e?S(e,l,{enumerable:!0,configurable:!0,writable:!0,value:a}):e[l]=a,_=(e,l)=>{for(var a in l||(l={}))x.call(l,a)&&E(e,a,l[a]);if(b)for(var a of b(l))y.call(l,a)&&E(e,a,l[a]);return e};import{_ as U}from"./index.1652784183534.js";import{a as k,a3 as L,m as M,ag as T,b as V,Z as o,S as u,a2 as s,o as g,e as D,Y as h,W as $,X as B}from"./vue.1652784183534.js";const z=k({name:"systemEditDept",setup(){const e=L({isShowDialog:!1,ruleForm:{deptLevel:[],deptName:"",person:"",phone:"",email:"",sort:0,status:!0,describe:""},deptData:[]}),l=m=>{m.deptLevel=["vueNextAdmin"],m.person="lyt",m.phone="12345678910",m.email="vueNextAdmin@123.com",e.ruleForm=m,e.isShowDialog=!0},a=()=>{e.isShowDialog=!1},i=()=>{a()},p=()=>{a()},c=()=>{e.deptData.push({deptName:"vueNextAdmin",createTime:new Date().toLocaleString(),status:!0,sort:Math.random(),describe:"\u9876\u7EA7\u90E8\u95E8",id:Math.random(),children:[{deptName:"IT\u5916\u5305\u670D\u52A1",createTime:new Date().toLocaleString(),status:!0,sort:Math.random(),describe:"\u603B\u90E8",id:Math.random()},{deptName:"\u8D44\u672C\u63A7\u80A1",createTime:new Date().toLocaleString(),status:!0,sort:Math.random(),describe:"\u5206\u90E8",id:Math.random()}]})};return M(()=>{c()}),_({openDialog:l,closeDialog:a,onCancel:i,onSubmit:p},T(e))}}),I={class:"system-edit-dept-container"},R={key:0},W={class:"dialog-footer"},X=B("\u53D6 \u6D88"),Y=B("\u4FEE \u6539");function Z(e,l,a,i,p,c){const m=s("el-cascader"),d=s("el-form-item"),n=s("el-col"),r=s("el-input"),N=s("el-input-number"),v=s("el-switch"),A=s("el-row"),w=s("el-form"),F=s("el-button"),C=s("el-dialog");return g(),V("div",I,[o(C,{title:"\u4FEE\u6539\u90E8\u95E8",modelValue:e.isShowDialog,"onUpdate:modelValue":l[8]||(l[8]=t=>e.isShowDialog=t),width:"769px"},{footer:u(()=>[D("span",W,[o(F,{onClick:e.onCancel,size:"default"},{default:u(()=>[X]),_:1},8,["onClick"]),o(F,{type:"primary",onClick:e.onSubmit,size:"default"},{default:u(()=>[Y]),_:1},8,["onClick"])])]),default:u(()=>[o(w,{model:e.ruleForm,size:"default","label-width":"90px"},{default:u(()=>[o(A,{gutter:35},{default:u(()=>[o(n,{xs:24,sm:24,md:24,lg:24,xl:24,class:"mb20"},{default:u(()=>[o(d,{label:"\u4E0A\u7EA7\u90E8\u95E8"},{default:u(()=>[o(m,{options:e.deptData,props:{checkStrictly:!0,value:"deptName",label:"deptName"},placeholder:"\u8BF7\u9009\u62E9\u90E8\u95E8",clearable:"",class:"w100",modelValue:e.ruleForm.deptLevel,"onUpdate:modelValue":l[0]||(l[0]=t=>e.ruleForm.deptLevel=t)},{default:u(({node:t,data:f})=>[D("span",null,h(f.deptName),1),t.isLeaf?$("",!0):(g(),V("span",R," ("+h(f.children.length)+") ",1))]),_:1},8,["options","modelValue"])]),_:1})]),_:1}),o(n,{xs:24,sm:12,md:12,lg:12,xl:12,class:"mb20"},{default:u(()=>[o(d,{label:"\u90E8\u95E8\u540D\u79F0"},{default:u(()=>[o(r,{modelValue:e.ruleForm.deptName,"onUpdate:modelValue":l[1]||(l[1]=t=>e.ruleForm.deptName=t),placeholder:"\u8BF7\u8F93\u5165\u90E8\u95E8\u540D\u79F0",clearable:""},null,8,["modelValue"])]),_:1})]),_:1}),o(n,{xs:24,sm:12,md:12,lg:12,xl:12,class:"mb20"},{default:u(()=>[o(d,{label:"\u8D1F\u8D23\u4EBA"},{default:u(()=>[o(r,{modelValue:e.ruleForm.person,"onUpdate:modelValue":l[2]||(l[2]=t=>e.ruleForm.person=t),placeholder:"\u8BF7\u8F93\u5165\u8D1F\u8D23\u4EBA",clearable:""},null,8,["modelValue"])]),_:1})]),_:1}),o(n,{xs:24,sm:12,md:12,lg:12,xl:12,class:"mb20"},{default:u(()=>[o(d,{label:"\u624B\u673A\u53F7"},{default:u(()=>[o(r,{modelValue:e.ruleForm.phone,"onUpdate:modelValue":l[3]||(l[3]=t=>e.ruleForm.phone=t),placeholder:"\u8BF7\u8F93\u5165\u624B\u673A\u53F7",clearable:""},null,8,["modelValue"])]),_:1})]),_:1}),o(n,{xs:24,sm:12,md:12,lg:12,xl:12,class:"mb20"},{default:u(()=>[o(d,{label:"\u90AE\u7BB1"},{default:u(()=>[o(r,{modelValue:e.ruleForm.email,"onUpdate:modelValue":l[4]||(l[4]=t=>e.ruleForm.email=t),placeholder:"\u8BF7\u8F93\u5165",clearable:""},null,8,["modelValue"])]),_:1})]),_:1}),o(n,{xs:24,sm:12,md:12,lg:12,xl:12,class:"mb20"},{default:u(()=>[o(d,{label:"\u6392\u5E8F"},{default:u(()=>[o(N,{modelValue:e.ruleForm.sort,"onUpdate:modelValue":l[5]||(l[5]=t=>e.ruleForm.sort=t),min:0,max:999,"controls-position":"right",placeholder:"\u8BF7\u8F93\u5165\u6392\u5E8F",class:"w100"},null,8,["modelValue"])]),_:1})]),_:1}),o(n,{xs:24,sm:12,md:12,lg:12,xl:12,class:"mb20"},{default:u(()=>[o(d,{label:"\u90E8\u95E8\u72B6\u6001"},{default:u(()=>[o(v,{modelValue:e.ruleForm.status,"onUpdate:modelValue":l[6]||(l[6]=t=>e.ruleForm.status=t),"inline-prompt":"","active-text":"\u542F","inactive-text":"\u7981"},null,8,["modelValue"])]),_:1})]),_:1}),o(n,{xs:24,sm:24,md:24,lg:24,xl:24,class:"mb20"},{default:u(()=>[o(d,{label:"\u90E8\u95E8\u63CF\u8FF0"},{default:u(()=>[o(r,{modelValue:e.ruleForm.describe,"onUpdate:modelValue":l[7]||(l[7]=t=>e.ruleForm.describe=t),type:"textarea",placeholder:"\u8BF7\u8F93\u5165\u90E8\u95E8\u63CF\u8FF0",maxlength:"150"},null,8,["modelValue"])]),_:1})]),_:1})]),_:1})]),_:1},8,["model"])]),_:1},8,["modelValue"])])}var H=U(z,[["render",Z]]);export{H as default};
